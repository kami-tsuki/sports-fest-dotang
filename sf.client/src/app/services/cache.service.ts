import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {lastValueFrom} from 'rxjs';

export interface CacheServiceConfig {
    cacheDuration: number;
    maxCacheSize?: number;
}

@Injectable({
    providedIn: 'root'
})
export class CacheService {
    private cache = new Map<string, { data: any; timestamp: number }>();
    private cacheDuration: number = 60000; // Default 1 minute
    private maxCacheSize?: number = 100;   // Default max size 100
    private readonly configFilePath = '/assets/cache.service.config.json';
    private readonly localStorageKey = 'CacheServiceConfig';

    constructor(private http: HttpClient) {
        this.initializeConfig().then(() => console.log('CacheService config loaded or created'));
    }

    private async initializeConfig(): Promise<void> {
        const localConfig = this.getConfigFromLocalStorage();

        if (localConfig) {
            this.applyConfig(localConfig);
            console.log('CacheService config loaded from local storage');
        } else {
            try {
                await this.loadConfig();
                console.log('CacheService config loaded from file');
            } catch (error) {
                this.createAndStoreDefaultConfig();
                console.log('CacheService config loaded from default');

            }
        }
    }

    private getConfigFromLocalStorage(): CacheServiceConfig | null {
        const configJson = localStorage.getItem(this.localStorageKey);
        return configJson ? JSON.parse(configJson) : null;
    }

    private saveConfigToLocalStorage(config: CacheServiceConfig): void {
        localStorage.setItem(this.localStorageKey, JSON.stringify(config));
    }

    private createAndStoreDefaultConfig(): void {
        const defaultConfig: CacheServiceConfig = {
            cacheDuration: this.cacheDuration,
            maxCacheSize: this.maxCacheSize,
        };
        this.saveConfigToLocalStorage(defaultConfig);
        this.applyConfig(defaultConfig);
        console.log('Default CacheService config created and stored in local storage');
    }

    private async loadConfig(): Promise<void> {
        const config: CacheServiceConfig = await lastValueFrom(
            this.http.get<CacheServiceConfig>(this.configFilePath)
        );
        this.saveConfigToLocalStorage(config);
        this.applyConfig(config);
    }

    private applyConfig(config: CacheServiceConfig): void {
        this.cacheDuration = config.cacheDuration;
        this.maxCacheSize = config.maxCacheSize;
    }

    setCache(key: string, data: any): void {
        this.ensureCacheSizeLimit();
        const timestamp = Date.now();
        this.cache.set(key, {data, timestamp});
        console.log(`CacheService: Cache set for key: ${key}`);
    }

    getCache<T>(key: string): T | null {
        const cached = this.cache.get(key);

        if (!cached) {
            console.log(`CacheService: Cache not found for key: ${key}`);
            return null;
        }

        const isExpired = Date.now() - cached.timestamp > this.cacheDuration;

        if (isExpired) {
            this.clearCache(key);
            console.log(`CacheService: Cache expired for key: ${key}`);
            return null;
        }

        console.log(`CacheService: Cache retrieved for key: ${key}`);
        return cached.data as T;
    }

    hasCache(key: string): boolean {
        return this.cache.has(key);
    }

    clearCache(key: string): void {
        this.cache.delete(key);
        console.log(`CacheService: Cache cleared for key: ${key}`);
    }

    clearAll(): void {
        this.cache.clear();
        console.log('CacheService: All cache cleared');
    }

    private ensureCacheSizeLimit(): void {
        if (this.maxCacheSize && this.cache.size >= this.maxCacheSize) {
            const oldestKey = Array.from(this.cache.keys())[0];
            this.clearCache(oldestKey);
            console.log(`CacheService: Cache size limit reached, oldest cache cleared for key: ${oldestKey}`);
        } else {
            console.log(`CacheService: Cache size: ${this.cache.size}`);
        }
    }
}
