import { useEffect, useState } from 'react';
import './App.css';


import  searchApi  from './service/searchService';
import  {useSearchCache}  from './hooks/useCache';
import { validateQuery } from './utils/helpers';
import type { SearchResponse } from './types/searchtypes';

import SearchBox from './Components/SearchBox';
import ErrorMessage from './Components/ErrorMessage';
import LoadingSpinner from './Components/Loader';
import  SearchResults  from './Components/SearchResult';

function App() {
    const [query, setQuery] = useState('');
    const [searchResults, setSearchResults] = useState<SearchResponse | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const { getCached, setCached } = useSearchCache();

    const handleSearch = async () => {
        
        const validation = validateQuery(query);
        if (!validation.valid) {
            setError(validation.error!);
            setQuery('');
            setSearchResults(null);
            return;
        }

        const cached = getCached(query);
        if (cached) {
            setSearchResults(cached);
            setError(null);
            return;
        }
        setLoading(true);
        setError(null);

        try {
            const data = await searchApi(query);
            setCached(query, data);
            setSearchResults(data);
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Failed to fetch results');
            setSearchResults(null);
        } finally {
            setLoading(false);
        }
    };


    useEffect(() => {
        setError(null);
        setSearchResults(null);

    },[query])

    return (
        <div className="container">
            <div className="card">
                <h1 className="title">Search Function</h1>
                <p className="subtitle">Search across Google and Wikipedia</p>

                <SearchBox
                    query={query}
                    onQueryChange={setQuery}
                    onSearch={handleSearch}
                    loading={loading}
                />

                {error && <ErrorMessage message={error} />}

                {loading && <LoadingSpinner />}

                {searchResults && (
                    <SearchResults
                        searchResults={searchResults}
                        loading={loading}
                    />
                )}
            </div>
        </div>
    );
}

export default App;