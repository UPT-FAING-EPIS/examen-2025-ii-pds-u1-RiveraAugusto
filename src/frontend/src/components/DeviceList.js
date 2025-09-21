import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';

const DeviceList = () => {
  const [devices, setDevices] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchDevices();
  }, []);

  const fetchDevices = async () => {
    try {
      setLoading(true);
      const response = await axios.get('http://localhost:5194/api/Cellphones');
      setDevices(response.data);
      setError(null);
    } catch (err) {
      setError('Error fetching devices. Please try again later.');
      console.error('Error fetching devices:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const filteredDevices = devices.filter(device => 
    device.imei.toLowerCase().includes(searchTerm.toLowerCase()) ||
    device.brand.toLowerCase().includes(searchTerm.toLowerCase()) ||
    device.model.toLowerCase().includes(searchTerm.toLowerCase()) ||
    device.status.toLowerCase().includes(searchTerm.toLowerCase()) ||
    device.location.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this device?')) {
      try {
        await axios.delete(`http://localhost:5194/api/Cellphones/${id}`);
        fetchDevices();
      } catch (err) {
        setError('Error deleting device. Please try again.');
        console.error('Error deleting device:', err);
      }
    }
  };

  if (loading) return <div className="text-center">Loading...</div>;
  if (error) return <div className="alert alert-danger">{error}</div>;

  return (
    <div>
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Devices Inventory</h2>
        <Link to="/devices/new" className="btn btn-primary">Add New Device</Link>
      </div>
      
      <div className="mb-3">
        <input
          type="text"
          className="form-control"
          placeholder="Search by IMEI, brand, model, status or location..."
          value={searchTerm}
          onChange={handleSearch}
        />
      </div>

      {filteredDevices.length === 0 ? (
        <div className="alert alert-info">No devices found.</div>
      ) : (
        <div className="table-responsive">
          <table className="table table-striped table-hover">
            <thead>
              <tr>
                <th>Brand</th>
                <th>Model</th>
                <th>IMEI</th>
                <th>Status</th>
                <th>Location</th>
                <th>Entry Date</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {filteredDevices.map(device => (
                <tr key={device.id}>
                  <td>{device.brand}</td>
                  <td>{device.model}</td>
                  <td>{device.imei}</td>
                  <td>{device.status}</td>
                  <td>{device.location}</td>
                  <td>{new Date(device.entryDate).toLocaleDateString()}</td>
                  <td>
                    <div className="btn-group">
                      <Link to={`/devices/${device.id}`} className="btn btn-sm btn-info">View</Link>
                      <Link to={`/devices/edit/${device.id}`} className="btn btn-sm btn-warning">Edit</Link>
                      <button onClick={() => handleDelete(device.id)} className="btn btn-sm btn-danger">Delete</button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};

export default DeviceList;