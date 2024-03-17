
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import './App.css';
import { HomePage, LoginPage, RegisterPage } from "./pages";

function App() {

    return (
        <BrowserRouter >
            <Routes>
                <Route path="/" element={<HomePage></HomePage>}></Route>
                <Route path="/register" element={<RegisterPage></RegisterPage>}></Route>
                <Route path="/login" element={<LoginPage></LoginPage>}></Route>
            </Routes>
        </BrowserRouter >
    )
}

export default App;