import { addresses } from "../urls";
import { Card } from "primereact/card";
import { Column } from "primereact/column";
import { Button } from "primereact/button";
import { WebSearchControl } from "./WebSearch";
import { DataTable } from "primereact/datatable";
import { InputText } from "primereact/inputtext";
import React, { useState, useEffect, useCallback } from "react";

export const Results = () => {
  let debounce;
  const [data, setData] = useState([]);
  const [filter, setFilter] = useState(null);
  const [typing, setTyping] = useState(false);
  const [loading, setLoading] = useState(true);
  const setFilterDebounced = useCallback(value => {
    setTyping(true);
    setFilter(value);
    debounce = setTimeout(() => setTyping(false), 500);
  }, [debounce]);

  useEffect(() => {
    if (!typing) {
      const url = filter
        ? `${addresses.ApiAddress}/query-results?filterTerm=${filter}`
        : `${addresses.ApiAddress}/query-results`;
  
      setLoading(true);
      fetch(url)
        .then(resp => resp.json())
        .then(data => { setData(data); setLoading(false) });
    }
  }, [filter, typing]);

  const frame = {
    "width": "100vw",
    "display": "grid",
    "minHeight": "100vh",
    "alignItems": "center",
    "justifyContent": "center",
    "gridTemplateRows": "5vh auto 5vh",
  };

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
            <InputText
              value={filter}
              style={{ marginLeft: 35 }}
              placeholder="Search previous results"
              onChange={(e) => setFilterDebounced(e.target.value)}
            />
          </div>
          <h1 style={{ fontSize: 22 }}>Search results</h1>
          {loading ? <h3>Loading...</h3> :
            <DataTable value={data}>
              <Column field="title" header="Title"></Column>
              <Column field="link" header="Url"></Column>
            </DataTable>}
        </Card>
      </div>
      <div />
    </div>
  )
}