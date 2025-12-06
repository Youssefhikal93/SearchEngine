import { formatNumber } from '../utils/helpers';

interface Props {
    total: number;
}

export default function TotalCard({ total }: Props) {
    return (
        <div className="total-card">
            <div className="total-label">Total Results (Sum)</div>
            <div className="total-value">{formatNumber(total)}</div>
        </div>
    );
};