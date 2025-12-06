import type { SearchResponse } from "../types/searchtypes";

const API_BASE_URL = 'http://localhost:5138/api';

export default async function searchApi (query: string): Promise<SearchResponse> {
     {
        const response = await fetch(`${API_BASE_URL}/Search`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ query: query.trim() }),
        });

        return response.json();
    }
};