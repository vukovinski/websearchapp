import AppRoutes from './AppRoutes';
import React, { Component } from 'react';
import { Layout } from './components/Layout';
import { Route, Routes } from 'react-router-dom';

import "primereact/resources/themes/saga-orange/theme.css";
import "primereact/resources/primereact.css";
import "primeicons/primeicons.css";
import './custom.css';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Routes>
          {AppRoutes.map((route, index) => {
            const { element, ...rest } = route;
            return <Route key={index} {...rest} element={element} />;
          })}
        </Routes>
      </Layout>
    );
  }
}
