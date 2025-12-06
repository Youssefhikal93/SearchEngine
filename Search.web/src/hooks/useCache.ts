import { useRef } from 'react';
import type { SearchResponse } from '../types/searchtypes';
import { normalizeQuery } from '../utils/helpers';


export function useSearchCache() {
    const cacheRef = useRef<{ [key: string]: SearchResponse }>({});

    const getCached = (query: string): SearchResponse | null => {
        const normalized = normalizeQuery(query);
        console.log('Cache HIT - rendering from cached words:', normalized);
        return cacheRef.current[normalized] || null;
    };

    const setCached = (query: string, data: SearchResponse): void => {
        const normalized = normalizeQuery(query);
        cacheRef.current[normalized] = data;
        console.log(` Cached: "${normalized}"}`);

    };
    return { getCached, setCached};
};
