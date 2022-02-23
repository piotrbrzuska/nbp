import React, { Component } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';

export class Home extends Component {
  static displayName = Home.name;

    constructor(props) {
        super(props);

        this.state = {
            rateDate: null,
            rates: null,
            selected: null,
            loading: true
        };
    }
    componentDidMount() {
        this.fetchExchangeRates();
    }
    
    async fetchExchangeRates() {
        const response = await fetch('/rates/last');
        const data = await response.json();
        const rates = data[0].rates.map( x=> { return {value: x.mid, currency: x.currency.currency, code: x.currency.code}});
        this.setState({ rates: rates, rateDate: new Date(data[0].effectiveDate) ,loading: false });
    }
    renderHeader() {
        const formattedDate = this.state.rateDate && this.state.rateDate.toLocaleDateString() || "";
        return (
            <div className="flex justify-content-between align-items-center">
                <h5 className="m-0">Exchange rate results of {formattedDate}</h5>
            </div>
        )
    }
    
  render () {
      const header = this.renderHeader();
    return (
      <div>

          <DataTable value={this.state.rates} paginator className="p-datatable-customers" header={header} rows={10}
                     paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown" rowsPerPageOptions={[10,25,50]}
                     dataKey="id" rowHover selection={this.state.selected} onSelectionChange={e => this.setState({ selected: e.value })}
                     filters={this.state.filters} filterDisplay="menu" loading={this.state.loading} responsiveLayout="scroll"
                     emptyMessage="No exchange rate results found."
                     currentPageReportTemplate="Showing {first} to {last} of {totalRecords} entries">
              <Column field="code" header="Currency Code" sortable filter filterPlaceholder="Search by Currency Code" />
              <Column field="currency" header="Currency" sortable filter filterPlaceholder="Search by Currency name" />
              <Column field="value" header="Value [zł]" />
          </DataTable>
      </div>
    );
  }
}
