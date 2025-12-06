export interface ProviderResult {
    providerName: string;
    searchTerm: string;
    wordHits?: Record<string, number>;
    totalHits: number;
    isSuccess: boolean;
    errorMessage: string | null;
    wordBreakdown: WordHit[]
}

export interface SearchResponse {
    query: string;
    searchTerm: string[];
    results: ProviderResult[];
}

export interface WordHit {
    word: string;
    hits: number;
}