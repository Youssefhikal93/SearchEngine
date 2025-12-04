import { useState, useRef } from 'react';
import './App.css';

interface ProviderResult {
    providerName: string;
    searchTerm: string;
    totalHits: number;
    isSuccess: boolean;
    errorMessage: string | null;
}

interface SearchResponse {
    query: string;
    searchTerm: string[];
    results: ProviderResult[];
}

//TODO
//every word to be cached instead of full word 
// max limit in the input 
function App() {
    const [query, setQuery] = useState('');
    const [searchResults, setSearchResults] = useState<SearchResponse | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const cacheRef = useRef<{ [key: string]: SearchResponse }>({});

    const formatNumber = (num: number): string => {
        return num.toLocaleString('en-US');
    };

    const getTotalSum = (): number => {
        if (!searchResults) return 0;
        return searchResults.results
            .filter(r => r.isSuccess)
            .reduce((sum, r) => sum + r.totalHits, 0);
    };

    const handleSearch = async () => {
        if (!query.trim()) {
            setError('Please enter a search query');
            setQuery('');
            setSearchResults(null);
            return;
        }

        const cleaned = query.trim().toLowerCase();
        if (cacheRef.current[cleaned]) {
            setSearchResults(cacheRef.current[cleaned]);
            return; 
        }
        setLoading(true);
        setError(null);

        try {
            const response = await fetch('http://localhost:5138/api/Search', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ query: query.trim() }),
            });

            if (!response.ok) {
                throw new Error(`API Error: ${response.status}`);
            }

            const data: SearchResponse = await response.json();

            cacheRef.current[cleaned] = data;

            setSearchResults(data);
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Failed to fetch results');
        } finally {
            setLoading(false);
        }
    };

    const handleKeyPress = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.key === 'Enter') {
            handleSearch();
        }
    };

    return (
        <div className="container">
            <div className="card">
                <h1 className="title">Search Aggregator</h1>
                <p className="subtitle">Search across multiple engines</p>

                
                <div className="search-box">
                    <input
                        type="text"
                        value={query}
                        onChange={(e) => setQuery(e.target.value)}
                        onKeyDown={handleKeyPress}
                        placeholder="Enter search terms (e.g., hello world)"
                        className="search-input"
                        disabled={loading}
                    />
                    <button
                        onClick={handleSearch}
                        className={`search-button ${loading ? 'disabled' : ''}`}
                        disabled={loading}
                    >
                        {loading ? 'Searching...' : 'Search'}
                    </button>
                </div>

               
                {error && (
                    <div className="error-message">
                        ⚠️ {error}
                    </div>
                )}

                
                {loading && (
                    <div className="loading-overlay">
                        <div className="spinner"></div>
                        <p>Searching engines...</p>
                    </div>
                )}

             
                {searchResults && (
                    <div className={`results ${loading ? 'loading' : ''}`}>
                        <div className="query-info">
                            <strong>Query:</strong> {searchResults.query}
                            <br />
                            <strong>Terms:</strong> {searchResults.searchTerm.join(', ')}
                        </div>

                        <div className="provider-results">
                            {searchResults.results.map((result, index) => (
                                <div
                                    key={index}
                                    className={`provider-card ${result.isSuccess ? 'success' : 'error'}`}
                                >
                                    <div className="provider-header">
                                        <span className="provider-name">{result.providerName}</span>
                                        <span className={`status-icon ${result.isSuccess ? 'success' : 'error'}`}>
                                            {result.isSuccess ? '✓' : '✗'}
                                        </span>
                                    </div>
                                    {result.isSuccess ? (
                                        <div className="hits">
                                            {formatNumber(result.totalHits)} hits
                                        </div>
                                    ) : (
                                        <div className="error-text">
                                            {result.errorMessage || 'Search failed'}
                                        </div>
                                    )}
                                </div>
                            ))}
                        </div>

                        {/* Total Sum */}
                        <div className="total-card">
                            <div className="total-label">Total Results (Sum)</div>
                            <div className="total-value">{formatNumber(getTotalSum())}</div>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
}

export default App;

