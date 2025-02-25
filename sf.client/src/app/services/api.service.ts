import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { CacheService } from './cache.service';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import {EntityOfGuid, IEntityOfGuid} from "@app/services/api/sf-client";

@Injectable({
    providedIn: 'root',
})
export class ApiService {
    constructor(
        private http: HttpClient,
        private readonly cacheService: CacheService
    ) {
        
    }
    public baseUrl = "api.sf.tsuki.wtf/api/";

    public get<T extends IEntityOfGuid | IEntityOfGuid[]>(
        key: string,
        params?: HttpParams,
        headers?: HttpHeaders,
        cacheLoad: boolean = true,
        cacheSet: boolean = true
    ): Observable<T> {
        if (this.cacheService.hasCache(this.baseUrl+key) && cacheLoad) {
            const cache = this.cacheService.getCache<T>(this.baseUrl+key);
            console.log(' loaded from cache');
            return of(cache as T);
        } else {
            return this.http.get<T>(this.baseUrl+key, { params, headers }).pipe(
                tap((result) => {
                    if (cacheSet) {
                        this.cacheService.setCache(this.baseUrl+key, result);
                        console.log(`ApiService: ${this.baseUrl+key} loaded from API and cached`);
                    } else {
                        console.log(`ApiService: ${this.baseUrl+key} loaded from API`);
                    }
                }),
                catchError((error) => {
                    console.error(`ApiService: ${this.baseUrl+key} failed to load from API`, error);
                    return of(undefined as unknown as T);
                })
            );
        }
    }
    
    

    public post<T extends IEntityOfGuid | IEntityOfGuid[]>(
        key: string,
        body: any,
        params?: HttpParams,
        headers?: HttpHeaders,
        cacheSet: boolean = false
    ): Observable<T> {
        return this.http.post<T>(this.baseUrl+key, body, { params, headers }).pipe(
            tap((result) => {
                if (cacheSet) {
                    this.cacheService.setCache(this.baseUrl+key, result);
                    console.log(`ApiService: ${this.baseUrl+key} posted to API and cached`);
                } else {
                    console.log(`ApiService: ${this.baseUrl+key} posted to API`);
                }
            }),
            catchError((error) => {
                console.error(`ApiService: ${this.baseUrl+key} failed to post to API`, error);
                return of(undefined as unknown as T);
            })
        );
    }

    public put<T extends IEntityOfGuid | IEntityOfGuid[]>(
        key: string,
        body: any,
        params?: HttpParams,
        headers?: HttpHeaders,
        cacheSet: boolean = false
    ): Observable<T> {
        return this.http.put<T>(this.baseUrl+key, body, { params, headers }).pipe(
            tap((result) => {
                if (cacheSet) {
                    this.cacheService.setCache(this.baseUrl+key, result);
                    console.log(`ApiService: ${this.baseUrl+key} put to API and cached`);
                } else {
                    console.log(`ApiService: ${this.baseUrl+key} put to API`);
                }
            }),
            catchError((error) => {
                console.error(`ApiService: ${this.baseUrl+key} failed to put to API`, error);
                return of(undefined as unknown as T);
            })
        );
    }

    public delete<T extends IEntityOfGuid | IEntityOfGuid[]>(
        key: string,
        body?: any,
        params?: HttpParams,
        headers?: HttpHeaders,
        cacheClear: boolean = true
    ): Observable<T> {
        return this.http.request<T>('delete', this.baseUrl+key, { body, params, headers }).pipe(
            tap((result) => {
                if (cacheClear) {
                    this.cacheService.clearCache(this.baseUrl+key);
                    console.log(`ApiService: ${this.baseUrl+key} deleted from API and cache cleared`);
                } else {
                    console.log(`ApiService: ${this.baseUrl+key} deleted from API`);
                }
            }),
            catchError((error) => {
                console.error(`ApiService: ${this.baseUrl+key} failed to delete from API`, error);
                return of(undefined as unknown as T);
            })
        );
    }

    public patch<T extends IEntityOfGuid | IEntityOfGuid[]>(
        key: string,
        body: any,
        params?: HttpParams,
        headers?: HttpHeaders,
        cacheSet: boolean = false
    ): Observable<T> {
        return this.http.patch<T>(this.baseUrl+key, body, { params, headers }).pipe(
            tap((result) => {
                if (cacheSet) {
                    this.cacheService.setCache(this.baseUrl+key, result);
                    console.log(`ApiService: ${this.baseUrl+key} patched to API and cached`);
                } else {
                    console.log(`ApiService: ${this.baseUrl+key} patched to API`);
                }
            }),
            catchError((error) => {
                console.error(`ApiService: ${this.baseUrl+key} failed to patch to API`, error);
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
        if (this.cacheService.hasCache(this.baseUrl+key) && cacheLoad) {
            const cache = this.cacheService.getCache<T>(key);
            console.log(' loaded from cache');
            return of(cache as T);
        } else {
            return this.http.options<T>(this.baseUrl+key, { params, headers }).pipe(
                tap((result) => {
                    if (cacheSet) {
                        this.cacheService.setCache(this.baseUrl+key, result);
                        console.log(`ApiService: ${this.baseUrl+key} options loaded from API and cached`);
                    } else {
                        console.log(`ApiService: ${this.baseUrl+key} options loaded from API`);
                    }
                }),
                catchError((error) => {
                    console.error(`ApiService: ${this.baseUrl+key} failed to load options from API`, error);
                    return of(undefined as unknown as T);
                })
            );
        }
    }

}
