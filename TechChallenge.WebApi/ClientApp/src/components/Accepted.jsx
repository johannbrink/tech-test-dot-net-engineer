import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faMapMarkerAlt, faBriefcase, faEnvelope, faPhone } from '@fortawesome/free-solid-svg-icons'
import Avatar from 'react-avatar';
import { ListGroup, ListGroupItem, Container, Row, Col  } from 'reactstrap';
import './Accepted.css';

export class Accepted extends Component {
  static displayName = Accepted.name;

  constructor(props) {
    super(props);
    this.state = { leads: [], loading: true };
  }

  componentDidMount() {
    this.populateLeadData();
  }

  static renderLeadsTable(leads) {
    return (
        <div>
          {leads.map(lead =>
          <ListGroup key={lead.id}>
            <ListGroupItem>
            <Container>
              <Row>
                <Col xs="0.5"><Avatar round size="50" name={lead.contact} /></Col>
                <Col xs="auto">{lead.contact}<br/>{new Intl.DateTimeFormat("en-AU", { year: "numeric", month: "long", day: "numeric", hour: "numeric", minute: "numeric" }).format(new Date(lead.dateCreated))}</Col>
              </Row>
            </Container>
            </ListGroupItem>
            <ListGroupItem>
              <Container>
                <Row>
                  <Col xs="0.5"><FontAwesomeIcon icon={faMapMarkerAlt} /></Col>
                  <Col xs="2">{lead.suburb} {lead.postCode}</Col>
                  <Col xs="0.5"><FontAwesomeIcon icon={faBriefcase} /></Col>
                  <Col xs="2">{lead.category} Job ID: {lead.id}</Col>
                  <Col xs="auto"><strong>{new Intl.NumberFormat("en-AU", { style: "currency", currency: "AUD" }).format(lead.price)}</strong> Lead Invitation</Col>
                </Row>
              </Container>
            </ListGroupItem>
            <ListGroupItem>
              <Container>
                <Row>
                  <Col xs="0.5"><FontAwesomeIcon icon={faPhone} /></Col>
                  <Col xs="2">{lead.phone}</Col>
                  <Col xs="0.5"><FontAwesomeIcon icon={faEnvelope} /></Col>
                  <Col xs="2">{lead.email}</Col>
                </Row>
              </Container>
            </ListGroupItem>
          <ListGroupItem className="description">{lead.description}</ListGroupItem>
          </ListGroup>
          )}
        </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Accepted.renderLeadsTable(this.state.leads);

    return (
      <div>
        {contents}
      </div>
    );
  }

  async populateLeadData() {
    const response = await fetch('lead/Accepted');
    const data = await response.json();
    this.setState({ leads: data, loading: false });
  }
}
