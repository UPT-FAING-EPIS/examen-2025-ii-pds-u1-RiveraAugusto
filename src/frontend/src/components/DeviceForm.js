import React, { useState, useEffect } from 'react';
import { Form, Button, Card, Alert } from 'react-bootstrap';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';

const DeviceForm = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(false);
  
  const [device, setDevice] = useState({
    brand: '',
    model: '',
    storage: 0,
    color: '',
    price: 0,
    releaseDate: '',
    isAvailable: true
  });

  useEffect(() => {
    if (id) {
      fetchDevice();
    }
  }, [id]);

  const fetchDevice = async () => {
    try {
      setLoading(true);
      const response = await axios.get(`http://localhost:5194/api/Cellphones/${id}`);
      setDevice(response.data);
      setError(null);
    } catch (err) {
      setError('Error fetching device details. Please try again.');
      console.error('Error fetching device:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setDevice({
      ...device,
      [name]: type === 'checkbox' ? checked : value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      setLoading(true);
      if (id) {
        await axios.put(`http://localhost:5194/api/Cellphones/${id}`, device);
      } else {
        await axios.post('http://localhost:5194/api/Cellphones', device);
      }
      setSuccess(true);
      setTimeout(() => {
        navigate('/');
      }, 2000);
    } catch (err) {
      setError('Error saving device. Please try again.');
      console.error('Error saving device:', err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Card>
      <Card.Header>{id ? 'Edit Device' : 'Add New Device'}</Card.Header>
      <Card.Body>
        {error && <Alert variant="danger">{error}</Alert>}
        {success && <Alert variant="success">Device saved successfully!</Alert>}
        
        <Form onSubmit={handleSubmit}>
          <Form.Group className="mb-3">
            <Form.Label>Brand</Form.Label>
            <Form.Control 
              type="text" 
              name="brand" 
              value={device.brand} 
              onChange={handleChange} 
              required 
            />
          </Form.Group>
          
          <Form.Group className="mb-3">
            <Form.Label>Model</Form.Label>
            <Form.Control 
              type="text" 
              name="model" 
              value={device.model} 
              onChange={handleChange} 
              required 
            />
          </Form.Group>
          
          <Form.Group className="mb-3">
            <Form.Label>Storage (GB)</Form.Label>
            <Form.Control 
              type="number" 
              name="storage" 
              value={device.storage} 
              onChange={handleChange} 
              required 
            />
          </Form.Group>
          
          <Form.Group className="mb-3">
            <Form.Label>Color</Form.Label>
            <Form.Control 
              type="text" 
              name="color" 
              value={device.color} 
              onChange={handleChange} 
              required 
            />
          </Form.Group>
          
          <Form.Group className="mb-3">
            <Form.Label>Price</Form.Label>
            <Form.Control 
              type="number" 
              step="0.01" 
              name="price" 
              value={device.price} 
              onChange={handleChange} 
              required 
            />
          </Form.Group>
          
          <Form.Group className="mb-3">
            <Form.Label>Release Date</Form.Label>
            <Form.Control 
              type="date" 
              name="releaseDate" 
              value={device.releaseDate ? device.releaseDate.split('T')[0] : ''} 
              onChange={handleChange} 
              required 
            />
          </Form.Group>
          
          <Form.Group className="mb-3">
            <Form.Check 
              type="checkbox" 
              label="Available" 
              name="isAvailable" 
              checked={device.isAvailable} 
              onChange={handleChange} 
            />
          </Form.Group>
          
          <Button variant="primary" type="submit" disabled={loading}>
            {loading ? 'Saving...' : 'Save Device'}
          </Button>
          <Button 
            variant="secondary" 
            onClick={() => navigate('/')} 
            className="ms-2"
          >
            Cancel
          </Button>
        </Form>
      </Card.Body>
    </Card>
  );
};

export default DeviceForm;