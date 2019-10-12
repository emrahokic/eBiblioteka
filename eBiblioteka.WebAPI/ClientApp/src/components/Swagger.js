import React, { Component } from 'react';

export class Swagger extends Component {
    displayName = Swagger.name

    render() {
        return (
            <div >
                <h1 style={{color:"grey"}}>Swagger</h1>

                <br />
                <br />
                <div className="kartica">
                    <h5>Upute za korištenje swagger-a:</h5>
                    <ul>
                        <br />
                        <li>Da bi ste koristili WEB API na Swagger stranici morate biti registrovani</li>
                        <br />
                        <li>Prvo unijeti podatke u dijelu <code >Basic authorization</code> </li>
                        <br />
                        <li>Username:   <code>vas username</code></li>
                        <br />
                        <li>Password: <code>vas password</code></li>
                        <br />
                        <li>Nakon unosa izvrsiti Autorizaciju na <h5>Korisnik <code>Get</code>  <code>/api/Korisnik/Auth</code></h5></li>
                        <br />
                        <li>U zavisnosti od uspjesne Autorizacije kopirati Token</li>
                        <br />
                        <li>Ponovno otici na <code>Authorize button</code></li>
                        <br />
                        <li>Izvrsiti Logout na <code>Basic authorization</code></li>
                        <br />
                        <li>U JWT Token Autorizaciji u polje <code>Value</code> upisati <code>Bearer <code>token</code></code> </li>

                    </ul>
                    <br />
                    <br />
                    <a style={{marginLeft: "25px"}} className="btn btn-primary" href="/swagger" target="blank">Swagger API Page</a>
                </div>
            </div>
        );
    }
}
