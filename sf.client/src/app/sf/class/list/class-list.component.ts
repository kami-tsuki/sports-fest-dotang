import { Component, OnInit } from '@angular/core';
import { Class, ResultModelOfPageOfClass } from "../../../services/api/sf-client";
import { ApiService } from "../../../services/api.service";
import { AppPaths } from "../../../app-paths";
import { DeleteModalComponent } from "../delete-modal/delete-modal.component";
import { EditAddModalComponent } from "../edit-add-modal/edit-add-modal.component";
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
    selector: 'sf-class-list',
    templateUrl: './class-list.component.html',
    styleUrls: ['./class-list.component.css']
})
export class ClassListComponent implements OnInit {
    public classes: Class[] = [];
    public paginatedClasses: Class[] = [];
    public pageSize = 10;
    public page = 1;
    public totalPages = 1;
    public totalEntities = 0;
    public searchText = '';
    private sortColumn: string | null = null;
    private sortDirection: 'asc' | 'desc' = 'asc';
    private classToDelete: Class | null = null;
    private searchSubject: Subject<string> = new Subject<string>();

    constructor(
        private readonly api: ApiService,
        private dialog: MatDialog,
        private route: ActivatedRoute,
        private router: Router
    ) {}

    ngOnInit() {
        this.route.queryParams.subscribe(params => {
            this.page = +params['page'] || 1;
            this.pageSize = +params['pageSize'] || 10;
            this.searchText = params['searchText'] || '';
            this.loadClasses();
        });

        this.searchSubject.pipe(
            debounceTime(3000),
            distinctUntilChanged()
        ).subscribe(searchText => {
            this.searchText = searchText;
            this.filterClasses();
        });

        console.log('ClassListComponent initialized');
    }

    updateQueryParams() {
        this.router.navigate([], {
            relativeTo: this.route,
            queryParams: {
                page: this.page,
                pageSize: this.pageSize,
                searchText: this.searchText
            },
            queryParamsHandling: 'merge'
        });
    }

    loadClasses(cache: boolean = true) {
        this.api.get<ResultModelOfPageOfClass>(`/api/v1/data/classes?page=${this.page}&entities=${this.pageSize}`, undefined, undefined, cache)
            .subscribe(result => {
                if (!result.data) {
                    console.error('ClassListComponent: Failed to load classes', result.error);
                    return;
                }
                this.classes = result.data?.data || [];
                this.totalEntities = result.data.total as number;
                this.totalPages = Math.ceil(this.totalEntities / this.pageSize);
                this.applyPagination();
                console.log('ClassListComponent: Classes loaded', this.classes);
            });
    }

    searchClasses() {
        this.api.get<ResultModelOfPageOfClass>(`/api/v1/data/classes/search?query=${this.searchText}&page=${this.page}&entities=${this.pageSize}`)
            .subscribe(result => {
                if (!result.data) {
                    console.error('ClassListComponent: Failed to search classes', result.error);
                    return;
                }
                this.classes = result.data?.data || [];
                this.totalEntities = result.data.total as number;
                this.totalPages = Math.ceil(this.totalEntities / this.pageSize);
                this.applyPagination();
                console.log('ClassListComponent: Classes searched', this.classes);
            });
    }

    applyPagination() {
        const filtered = this.classes.filter(item =>
            item.id?.toString().includes(this.searchText) ||
            item.short?.includes(this.searchText) ||
            item.name?.includes(this.searchText) ||
            item.comment?.includes(this.searchText)
        );
        this.paginatedClasses = this.sort(filtered);
        console.log('ClassListComponent: Pagination applied', this.paginatedClasses);
    }

    filterClasses() {
        this.page = 1;
        this.searchClasses();
        console.log('ClassListComponent: Classes filtered', this.paginatedClasses);
    }

    sort(data: Class[]): Class[] {
        if (!this.sortColumn) return data;
        return data.sort((a, b) => {
            const aValue = a[this.sortColumn as keyof Class] || '';
            const bValue = b[this.sortColumn as keyof Class] || '';
            console.log('ClassListComponent: Sorting', aValue, bValue);
            return (aValue > bValue ? 1 : -1) * (this.sortDirection === 'asc' ? 1 : -1);
        });
    }

    sortCol(column: string) {
        if (this.sortColumn === column) {
            this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            this.sortColumn = column;
            this.sortDirection = 'asc';
        }
        this.filterClasses();
        console.log('ClassListComponent: Sorting', this.sortColumn, this.sortDirection);
    }

    nextPage() {
        if (this.page < this.totalPages) {
            this.page++;
            this.loadClasses(false);
            console.log('ClassListComponent: Next page', this.page);
        } else {
            console.log('ClassListComponent: No next page', this.page, this.totalPages);
        }
    }

    prevPage() {
        if (this.page > 1) {
            this.page--;
            this.loadClasses(false);
            console.log('ClassListComponent: Previous page', this.page);
        } else {
            console.log('ClassListComponent: No previous page', this.page);
        }
    }

    refreshData(cache: boolean = true) {
        this.loadClasses(cache);
        console.log('ClassListComponent: Refresh data');
    }

    edit(item: Class) {
        const dialogRef = this.dialog.open(EditAddModalComponent, {
            width: '400px',
            data: {class: {...item}, isEdit: true}
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.api.put(`/api/v1/data/classes/${result.id}`, result)
                    .subscribe(() => {
                        console.log('ClassListComponent: Class updated', result);
                        this.refreshData(false);
                    });
            }
        });
    }

    add() {
        const dialogRef = this.dialog.open(EditAddModalComponent, {
            width: '400px',
            data: {class: new Class(), isEdit: false}
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.api.post('/api/v1/data/classes', result)
                    .subscribe(() => {
                        console.log('ClassListComponent: Class added', result);
                        this.refreshData(false);
                    });
            }
        });
    }

    delete(item: Class) {
        this.classToDelete = item;
        const dialogRef = this.dialog.open(DeleteModalComponent, {
            width: '250px',
            data: {classToDelete: this.classToDelete}
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.confirmDelete();
            } else {
                this.cancelDelete();
            }
        });
    }

    confirmDelete() {
        if (this.classToDelete) {
            console.log('ClassListComponent: Delete ' + this.classToDelete.id, this.classToDelete);
            this.api.delete(`/api/v1/data/classes/${this.classToDelete.id}`)
                .subscribe(() => {
                    console.log('ClassListComponent: Class deleted', this.classToDelete?.id);
                    this.refreshData(false);
                });
            this.classToDelete = null;
        }
    }

    cancelDelete() {
        this.classToDelete = null;
    }

    onPageSizeChange() {
        this.page = 1;
        this.loadClasses(false);
    }

    onSearchTextChange(searchText: string) {
        this.searchSubject.next(searchText);
    }

    onSearchKeyPress(event: KeyboardEvent) {
        if (event.key === 'Enter') {
            this.searchSubject.next(this.searchText);
        }
    }

    protected readonly AppPaths = AppPaths;

    title = 'Sports Fest | Classes';
}