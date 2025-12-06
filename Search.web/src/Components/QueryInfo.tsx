
interface Props {
    query: string;
    terms: string[];
   
}

export function QueryInfo ({ query,terms }:Props) {
    return (
        <div className="query-info">
            <strong>Query:</strong> {query}
            <br/>
            <strong>Terms:</strong> {terms.join(', ')}
        </div>
    );
};