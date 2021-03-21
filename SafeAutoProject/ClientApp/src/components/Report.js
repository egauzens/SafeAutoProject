import React, { Component } from 'react';

export class Report extends Component {
    static displayName = Report.name;

    constructor(props) {
        super(props);
        this.state = { drivers: [], loading: false, error: "" };
    }

    renderReport() {
        this.state.drivers.sort((a, b) => (a.totalMiles > b.totalMiles) ? -1 : 1);

        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Miles</th>
                        <th>MPH</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.drivers.map(driver =>
                        <tr key={driver.name}>
                            <td>{driver.name}</td>
                            <td>{Math.round(driver.totalMiles)}</td>
                            <td>{Math.round(driver.averageMph)}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let content;
        if (this.state.error !== "") {
            content = <p className={ "text-danger" }><em>{this.state.error}</em></p>
        }
        else {
            content = this.state.loading
                ? <p><em>Generating Report...</em></p>
                : this.renderReport();
        }

        return (
            <div>
                <h1>Report Generator</h1>
                {content}
                <button className="btn btn-primary" onClick={() => this.generateReport()}>Generate Report</button>
            </div>
        );
    }

    async generateReport() {
        this.setState({ loading: true });
        const response = await fetch('drivers');
        const data = await response.json();
        if (data.statusCode !== 200) {
            this.setState({ error: data.value, loading: false });
        }
        else
        {
            this.setState({ drivers: data.value, loading: false });
        }
    }

    
}
