import React from 'react';
import { Card, Alert } from 'react-bootstrap';

const Reports = () => {
  return (
    <Card>
      <Card.Header>Reports</Card.Header>
      <Card.Body>
        <Alert variant="info">
          El módulo de reportes está en desarrollo. Estará disponible próximamente.
        </Alert>
      </Card.Body>
    </Card>
  );
};

export default Reports;