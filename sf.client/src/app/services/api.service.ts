import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { CacheService } from './cache.service';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import {
    IResultModelOfEntry,
    IResultModelOfObject,
    ResultModelOfClass,
    ResultModelOfDiscipline,
    ResultModelOfEntry,
    ResultModelOfLocation,
    ResultModelOfLong,
    ResultModelOfObject,
    ResultModelOfPageOfClass,
    ResultModelOfPageOfDiscipline,
    ResultModelOfPageOfEntry,
    ResultModelOfPageOfUser,
    ResultModelOfUser,
    IResultModelOfClass,
    IResultModelOfDiscipline,
    IResultModelOfLocation,
    IResultModelOfPageOfClass,
    IResultModelOfPageOfDiscipline,
    IResultModelOfPageOfEntry,
    IResultModelOfPageOfUser,
    IResultModelOfLong,
    IResultModelOfUser,
} from './api/sf-client';

type ApiResultType =
    ResultModelOfObject
    | ResultModelOfClass
    | ResultModelOfPageOfClass
    | ResultModelOfLong
    | ResultModelOfEntry
    | ResultModelOfDiscipline
    | ResultModelOfLocation
    | ResultModelOfPageOfDiscipline
    | ResultModelOfPageOfEntry
    | ResultModelOfPageOfUser
    | ResultModelOfUser
    | IResultModelOfObject
    | IResultModelOfClass
    | IResultModelOfPageOfClass
    | IResultModelOfLong
    | IResultModelOfEntry
    | IResultModelOfDiscipline
    | IResultModelOfLocation
    | IResultModelOfPageOfDiscipline
    | IResultModelOfPageOfEntry
    | IResultModelOfPageOfUser
    | IResultModelOfUser;
@Injectable({
    providedIn: 'root',
})
export class ApiService {
    constructor(
        private http: HttpClient,
        private readonly cacheService: CacheService
    ) {}

    public get<T extends ApiResultType>(
        key: string,
        params?: HttpParams,
        headers?: HttpHeaders,
        cacheLoad: boolean = true,
        cacheSet: boolean = true
    ): Observable<T> {
        if (this.cacheService.hasCache(key) && cacheLoad) {
            const cache = this.cacheService.getCache<T>(key);
            console.log(' loaded from cache');
            return of(cache as T);
        } else {
            return this.http.get<T>(key, { params, headers }).pipe(
                tap((result) => {
                    if (cacheSet) {
                        this.cacheService.setCache(key, result);
                        console.log(`ApiService: ${key} loaded from API and cached`);
                    } else {
                        console.log(`ApiService: ${key} loaded from API`);
                    }
                }),
                catchError((error) => {
                    console.error(`ApiService: ${key} failed to load from API`, error);
                    return of(undefined as unknown as T);
                })
            );
        }
    }
    
    

    public post<T extends ApiResultType>(
        key: string,
        body: any,
        params?: HttpParams,
        headers?: HttpHeaders,
        cacheSet: boolean = false
    ): Observable<T> {
        return this.http.post<T>(key, body, { params, headers }).pipe(
            tap((result) => {
                if (cacheSet) {
                    this.cacheService.setCache(key, result);
                    console.log(`ApiService: ${key} posted to API and cached`);
                } else {
                    console.log(`ApiService: ${key} posted to API`);
                }
            }),
            catchError((error) => {
                console.error(`ApiService: ${key} failed to post to API`, error);
                return of(undefined as unknown as T);
            })
        );
    }

    public put<T extends ApiResultType>(
        key: string,
        body: any,
        params?: HttpParams,
        headers?: HttpHeaders,
        cacheSet: boolean = false
    ): Observable<T> {
        return this.http.put<T>(key, body, { params, headers }).pipe(
            tap((result) => {
                if (cacheSet) {
                    this.cacheService.setCache(key, result);
                    console.log(`ApiService: ${key} put to API and cached`);
                } else {
                    console.log(`ApiService: ${key} put to API`);
                }
            }),
            catchError((error) => {
                console.error(`ApiService: ${key} failed to put to API`, error);
                return of(undefined as unknown as T);
            })
        );
    }

   public delete<T extends  ApiResultType>(
        key: string,
        params?: HttpParams,
        headers?: HttpHeaders,
        cacheClear: boolean = true
    ): Observable<T> {
        return this.http.delete<T>(key, { params, headers }).pipe(
            tap((result) => {
                if (cacheClear) {
                    this.cacheService.clearCache(key);
                    console.log(`ApiService: ${key} deleted from API and cache cleared`);
                } else {
                    console.log(`ApiService: ${key} deleted from API`);
                }
            }),
            catchError((error) => {
                console.error(`ApiService: ${key} failed to delete from API`, error);
                return of(undefined as unknown as T);
            })
        );
    }

    public patch<T extends ApiResultType>(
        key: string,
        body: any,
        params?: HttpParams,
        headers?: HttpHeaders,
        cacheSet: boolean = false
    ): Observable<T> {
        return this.http.patch<T>(key, body, { params, headers }).pipe(
            tap((result) => {
                if (cacheSet) {
                    this.cacheService.setCache(key, result);
                    console.log(`ApiService: ${key} patched to API and cached`);
                } else {
                    console.log(`ApiService: ${key} patched to API`);
                }
            }),
            catchError((error) => {
                console.error(`ApiService: ${key} failed to patch to API`, error);
                return of(undefined as unknown as T);
            })
        );
    }

    public options<T>(
        key: string,
        params?: HttpParams,
        headers?: HttpHeaders,
        cacheLoad: boolean = false,
        cacheSet: boolean = false
    ): Observable<T> {
        if (this.cacheService.hasCache(key) && cacheLoad) {
            const cache = this.cacheService.getCache<T>(key);
            console.log(' loaded from cache');
            return of(cache as T);
        } else {
            return this.http.options<T>(key, { params, headers }).pipe(
                tap((result) => {
                    if (cacheSet) {
                        this.cacheService.setCache(key, result);
                        console.log(`ApiService: ${key} options loaded from API and cached`);
                    } else {
                        console.log(`ApiService: ${key} options loaded from API`);
                    }
                }),
                catchError((error) => {
                    console.error(`ApiService: ${key} failed to load options from API`, error);
                    return of(undefined as unknown as T);
                })
            );
        }
    }
}
