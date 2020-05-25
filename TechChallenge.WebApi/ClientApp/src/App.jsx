import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Invited } from './components/Invited';
import { Accepted } from './components/Accepted';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Invited} />
        <Route path='/invited' component={Invited} />
        <Route path='/accepted' component={Accepted} />
      </Layout>
    );
  }
}
