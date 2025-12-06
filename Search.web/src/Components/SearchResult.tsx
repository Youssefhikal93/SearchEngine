
import { QueryInfo } from './QueryInfo';

import TotalCard  from './TotalCard';
import ProviderCard from './ProviderCard';
import type { SearchResponse } from '../types/searchtypes';

interface Props {
    searchResults: SearchResponse;
    loading: boolean;
}

export default function SearchResults({ searchResults, loading }: Props) {

    function getTotalSum (): number {
        return searchResults.results
            .filter(r => r.isSuccess)
            .reduce((sum, r) => sum + r.totalHits, 0);
    };

    return (
        <div className={`results ${loading ? 'loading' : ''}`}>
            <QueryInfo
                query={searchResults.query}
                terms={searchResults.searchTerm}
                
            />

            <div className="provider-results">
                {searchResults.results.map((result, index) => (
                    <ProviderCard key={index} result={result} />
                ))}
            </div>

            <TotalCard total={getTotalSum()} />
        </div>
    );
};