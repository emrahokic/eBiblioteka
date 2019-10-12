import React, { Component, Fragment } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import  LoadingScreen  from 'react-loading-screen';
import { Swagger } from './components/Swagger';
class App extends Component {
    displayName = App.name
    constructor(props) {
        super(props);
        this.state = { loading: true };
    }
    componentDidMount() {
        
        this.timerID = setInterval(
            () => this.tick(),
            2500
        );
    }

    componentWillUnmount() {
        clearInterval(this.timerID);
    }

    tick() {
        this.setState({
            loading: false
        });
    }
   
    render() {

        return (
            <Fragment>
                {
                    this.state.loading ? <LoadingScreen loading={true} bgColor='#409AB2' spinnerColor='#fff' logoSrc='logo.png' textColor='#ffffff'   text='eBiblioteka' /> : <Layout >
                        <Route exact path='/' component={Home} />
                        <Route path='/counter' component={Counter} />
                        <Route path='/fetchdata' component={FetchData} />
                        <Route path='/swagger_api' component={Swagger} />
                    </Layout >
                }
            </Fragment>
    );
    }
}
export default App;