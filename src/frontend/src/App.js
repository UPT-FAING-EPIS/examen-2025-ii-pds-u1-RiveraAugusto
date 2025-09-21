import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

// Components
import Navigation from './components/Navigation';
import DeviceList from './components/DeviceList';
import DeviceForm from './components/DeviceForm';
import DeviceDetail from './components/DeviceDetail';
import MovementForm from './components/MovementForm';
import Reports from './components/Reports';

function App() {
  return (
    <Router>
      <div className="App">
        <Navigation />
        <div className="container mt-4">
          <Routes>
            <Route path="/" element={<DeviceList />} />
            <Route path="/devices/new" element={<DeviceForm />} />
            <Route path="/devices/edit/:id" element={<DeviceForm />} />
            <Route path="/devices/:id" element={<DeviceDetail />} />
            <Route path="/movements/new" element={<MovementForm />} />
            <Route path="/reports" element={<Reports />} />
          </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App;