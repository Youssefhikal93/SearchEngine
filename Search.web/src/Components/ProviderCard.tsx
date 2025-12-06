import type { ProviderResult } from '../types/searchtypes';
import { formatNumber } from '../utils/helpers';

interface Props {
    result: ProviderResult;
}

export default function ProviderCard({ result }: Props) {
    console.log(result)
    return (
        <div className={`provider-card ${result.isSuccess ? 'success' : 'error'}`}>
            <div className="provider-header">
                <span className="provider-name">{result.providerName}</span>
                <span className={`status-icon ${result.isSuccess ? 'success' : 'error'}`}>
                    {result.isSuccess ? '✓' : '✗'}
                </span>
            </div>

            {result.isSuccess ? (
                <div>
                   
                    <div className="hits">
                        {formatNumber(result.totalHits)} hits
                    </div>

                    {result.wordBreakdown &&  (
                        <div className="word-breakdown">
                            {result.wordBreakdown.map((wordHit, index) => (
                                <div key={index} className="word-hit">
                                    <span className="word-label">{wordHit.word}:</span>
                                    <span className="word-count">
                                        {formatNumber(wordHit.hits)} hits
                                    </span>
                                </div>
                            ))}
                        </div>
                    )}
                </div>
            ) : (
                <div className="error-text">
                    {result.errorMessage || 'Search failed'}
                </div>
            )}
        </div>
    );
}