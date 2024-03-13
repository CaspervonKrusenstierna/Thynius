
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css';
import HomePage from './pages/homepage/HomePage';
import React from 'react';
function App(props) {
  return (
    <Router>
      <Routes>
          <Route path="/" element={<HomePage/>} />
      </Routes>
    </Router>
  );
}

export default App;
