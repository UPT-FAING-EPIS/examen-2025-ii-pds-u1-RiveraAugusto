import React, { useState, useEffect } from 'react';
import { Card, Button, Alert, Row, Col } from 'react-bootstrap';
import { useParams, useNavigate, Link } from 'react-router-dom';
import axios from 'axios';

const DeviceDetail = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [device, setDevice] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchDevice();
  }, []);

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

  const handleDelete = async () => {
    if (window.confirm('Are you sure you want to delete this device?')) {
      try {
        await axios.delete(`http://localhost:5194/api/Cellphones/${id}`);
        navigate('/');
      } catch (err) {
        setError('Error deleting device. Please try again.');
        console.error('Error deleting device:', err);
      }
    }
  };

  if (loading) return <div className="text-center">Loading...</div>;
  if (error) return <Alert variant="danger">{error}</Alert>;
  if (!device) return <Alert variant="warning">Device not found</Alert>;

  return (
    <Card>
      <Card.Header className="d-flex justify-content-between align-items-center">
        <span>Device Details</span>
        <div>
          <Link to={`/devices/edit/${id}`} className="btn btn-primary me-2">Edit</Link>
          <Button variant="danger" onClick={handleDelete}>Delete</Button>
        </div>
      </Card.Header>
      <Card.Body>
        <Row>
          <Col md={6}>
            <p><strong>Brand:</strong> {device.brand}</p>
            <p><strong>Model:</strong> {device.model}</p>
            <p><strong>Storage:</strong> {device.storage} GB</p>
            <p><strong>Color:</strong> {device.color}</p>
          </Col>
          <Col md={6}>
            <p><strong>Price:</strong> ${device.price.toFixed(2)}</p>
            <p><strong>Release Date:</strong> {new Date(device.releaseDate).toLocaleDateString()}</p>
            <p><strong>Status:</strong> {device.isAvailable ? 'Available' : 'Not Available'}</p>
          </Col>
        </Row>
        <div className="mt-3">
          <Button variant="secondary" onClick={() => navigate('/')}>Back to List</Button>
        </div>
      </Card.Body>
    </Card>
  );
};

export default DeviceDetail;