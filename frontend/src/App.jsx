import Header from './components/Header';
import Home from './pages/Home';
import './App.css';

function App() {
    return (
        <div className="app">
            <Header />
            <main className="app-main">
                <Home />
            </main>
        </div>
    );
}

export default App;
