import { addresses } from "../urls";
import React, { useState } from "react";
import { Card } from "primereact/card";
import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";


export const WebSearchControl = (props) => {
  const [searchTerm, setSearchTerm] = useState(props.query ?? "");
  return (
      <form name="webSearchForm" method="get" action={`${addresses.ApiAddress}/query`}>
        <h1 style={{ fontSize: 23 }}>Frontier Search</h1>
        <InputText
          name="webSearch"
          value={searchTerm}
          style={{ minWidth: "40vw" }}
          placeholder="Explore beyond the possibilities..."
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        <Button
          label="Search"
          iconPos="right"
          icon="pi pi-search"
          style={{ marginLeft: "15px" }}
          onClick={() => document.webSearchForm.submit()}
        />
      </form>
  );
};

export const WebSearchPage = () => {
  const frame = {
    "width": "100vw",
    "display": "grid",
    "minHeight": "100vh",
    "alignItems": "center",
    "justifyContent": "center",
    "gridTemplateRows": "25vh auto 25vh",
  };

  const form = {
    "max-width": "66vw"
  };

  return (
    <div style={frame} className="background">
      <div />
      <div style={form}>
        <Card style={{ padding: 35 }}>
          <WebSearchControl />
        </Card>
      </div>
      <div />
    </div>
  );
};
