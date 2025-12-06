export const MAX_QUERY_LENGTH = 100;

export function validateQuery (query: string): { valid: boolean; error?: string } {
    if (!query.trim()) {
        return { valid: false, error: 'Please enter a search query' };
    }

    if (query.trim().length > MAX_QUERY_LENGTH) {
        return {
            valid: false,
            error: `Query too long (max ${MAX_QUERY_LENGTH} characters)`
        };
    }

    return { valid: true };
};

export function formatNumber (num: number): string{
    return num.toLocaleString('en-US');
};

export function normalizeQuery (query: string): string {
    return query.trim().toLowerCase().replace(/\s+/g, ' ');
};