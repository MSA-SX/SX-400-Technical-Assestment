import './Header.css';

function Header() {
    return (
        <header className="header">
            <div className="header-inner">
                <div className="header-brand">
                    <div className="header-logo">
                        <span className="logo-icon">⚡</span>
                    </div>
                    <div>
                        <h1 className="header-title">SX400</h1>
                        <p className="header-subtitle">Dashboard</p>
                    </div>
                </div>

                <nav className="header-nav">
                    <a href="#" className="nav-link active">Home</a>
                    <a href="#" className="nav-link">API Docs</a>
                </nav>

                <div className="header-status">
                    <span className="status-dot"></span>
                    <span className="status-text">System Online</span>
                </div>
            </div>
        </header>
    );
}

export default Header;
