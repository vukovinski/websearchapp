import { addresses } from "../urls";
import { Card } from "primereact/card";
import { Column } from "primereact/column";
import { Button } from "primereact/button";
import { WebSearchControl } from "./WebSearch";
import { DataTable } from "primereact/datatable";
import { InputText } from "primereact/inputtext";
import React, { useState, useEffect, useCallback } from "react";

export const Results = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const rel = new URL(window.location).searchParams.get("rel");

  useEffect(() => {
    const queryString =
      rel ? `query-results?rel=${rel}` : `query-results`;
    const url = `${addresses.ApiAddress}/${queryString}`;

    setLoading(true);
    fetch(url)
      .then(resp => resp.json())
      .then(data => { setData(data); setLoading(false) });
  }, []);

  const frame = {
    "width": "100vw",
    "display": "grid",
    "minHeight": "100vh",
    "alignItems": "center",
    "justifyContent": "center",
    "gridTemplateRows": "5vh auto 5vh",
  };

  const titleColumnTemplate = (rowData) => {
    return (
      <div>
        <a href={rowData.link} target="_blank" rel="noopener">{rowData.title}</a>
      </div>
    )
  }

  return (
    <div style={frame} className="background">
      <div />
      <div>
        <Card style={{ padding: 35, width: "88vw" }}>
          <WebSearchControl />
          <div style={{ marginTop: 35, marginBottom: 35 }}>
            <Button
              iconPos="right"
              label="Back to home"
              icon="pi pi-arrow-left"
              onClick={() => window.location.href = addresses.AppAddress}
            />
          </div>
          {loading ? <h3>Loading...</h3> :
            <DataTable value={data}>
              <Column header="Search results" body={titleColumnTemplate}></Column>
            </DataTable>}
        </Card>
      </div>
      <div />
    </div>
  )
}