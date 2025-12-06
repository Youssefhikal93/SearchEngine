import { MAX_QUERY_LENGTH } from '../utils/helpers';

interface Props {
    query: string;
    onQueryChange: (query: string) => void;
    onSearch: () => void;
    loading: boolean;
}

export default function SearchBox({ query, onQueryChange, onSearch, loading }: Props){

    function handleKeyPress  (e: React.KeyboardEvent<HTMLInputElement>) {
        if (e.key === 'Enter') {
            onSearch();
        }
    };

    const remainingChars = MAX_QUERY_LENGTH - query.length;

    const isNearLimit = remainingChars <= 20;
    const limitFinished = remainingChars == 0;

    return (
        <div>
            <div className="search-box">
                <input
                    type="text"
                    value={query}
                    onChange={(e) => onQueryChange(e.target.value)}
                    onKeyDown={handleKeyPress}
                    placeholder="Search about something (e.g., I am Voyader)"
                    className="search-input"
                    disabled={loading}
                    maxLength={MAX_QUERY_LENGTH}
                />
                <button
                    onClick={onSearch}
                    className={`search-button ${loading ? 'disabled' : ''}`}
                    disabled={loading || limitFinished}
                >
                    {loading ? 'Searching...' : 'Search'}
                </button>
            </div>
            {query.length > 0 && (
                <div className={`char-counter ${limitFinished ? "danger" : isNearLimit ? 'warning' : ''}`}>
                    {remainingChars} characters remaining
                </div>
            )}
        </div>
    );
};