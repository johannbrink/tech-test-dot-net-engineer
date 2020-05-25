import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faMapMarkerAlt, faBriefcase } from '@fortawesome/free-solid-svg-icons'
import Avatar from 'react-avatar';
import { ListGroup, ListGroupItem, Container, Row, Col  } from 'reactstrap';
import './Invited.css';

export class Invited extends Component {
  static displayName = Invited.name;

  constructor(props) {
    super(props);
    this.state = { leads: [], loading: true };
    this.accept = this.accept.bind(this);
    this.decline = this.decline.bind(this);
  }

  componentDidMount() {
    this.populateLeadData();
  }

  async accept(leadId) {
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ leadId: leadId })
    };
    await fetch('/lead/accept', requestOptions);
    await this.populateLeadData();
  }

  async decline(leadId) {
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ leadId: leadId })
    };
    await fetch('/lead/decline', requestOptions);
    await this.populateLeadData();
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : 
      <div>
          {this.state.leads.map(lead =>
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
                <Col xs="2">{lead.suburb} {lead.postCode} </Col>
                <Col xs="0.5"><FontAwesomeIcon icon={faBriefcase} /></Col>
                <Col xs="auto">{lead.category} Job ID: {lead.id} </Col>
              </Row>
            </Container>
          </ListGroupItem>
          <ListGroupItem className="description">{lead.description}</ListGroupItem>
          <ListGroupItem>
            <Container>
              <Row>
                <Col xs="2.5"><button className="btn btn-primary" onClick={() => this.accept(lead.id)}>Accept</button> <button className="btn btn-secondary" onClick={() => this.decline(lead.id)}>Decline</button></Col>
                <Col xs="auto"><strong>{new Intl.NumberFormat("en-AU", { style: "currency", currency: "AUD" }).format(lead.price)}</strong> Lead Invitation</Col>
              </Row>
            </Container>
          </ListGroupItem>
        </ListGroup>          
          )}
        </div>;

    return (
      <div>
        {contents}
      </div>
    );
  }

  async populateLeadData() {
    const response = await fetch('lead/invited');
    const data = await response.json();
    this.setState({ leads: data, loading: false });
  }
}
