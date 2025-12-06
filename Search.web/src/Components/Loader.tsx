export default function LoadingSpinner() { 
    return (
        <div className="loading-overlay">
            <div className="spinner"></div>
            <p>Searching engines for hits...</p>
        </div>
    );
};