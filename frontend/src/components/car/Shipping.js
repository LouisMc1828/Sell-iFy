import React, { Fragment, useEffect, useState } from 'react'
import { useAlert } from 'react-alert'
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import MetaData from '../layout/MetaData';
import { resetUpdateStatus } from '../../slices/SecuritySlice';
import { saveAddressInfo } from '../../actions/CarAction';
import { CheckoutSteps } from './CheckoutSteps';

const Shipping = () => {

    const alert = useAlert();
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { dirEnvio, isUpdated, errores } = useSelector(state => state.security);
    const { countries } = useSelector(state => state.country);

    const [direccion, setDir] = useState( dirEnvio ? dirEnvio.direccion : "");
    const [ciudad, setCiudad] = useState( dirEnvio ? dirEnvio.ciudad : "");
    const [departamento, setDepartamento] = useState( dirEnvio ? dirEnvio.departamento : "");
    const [codigoPostal, setCodigoPostal] = useState( dirEnvio ? dirEnvio.codigoPostal : "");
    const [pais, setPais] = useState( dirEnvio ? dirEnvio.pais : "US");

    useEffect( () => {
        if(isUpdated)
        {
            navigate ("/order/confirm");
            dispatch(resetUpdateStatus({}));
        }

        if(errores)
        {
            errores.map(error => alert.error(error));
        }
    }, [dispatch, errores, alert, navigate, isUpdated]);

    const submitHandler = (e) => {
        e.preventDefault();
        const request = {
            direccion,
            ciudad,
            departamento,
            codigoPostal,
            pais
        };
        dispatch(saveAddressInfo(request));
    }






  return (
    <Fragment>
    <MetaData titulo={"Informacion De Envio"}/>
    <CheckoutSteps envio/>
         <div className="row wrapper">
                <div className="col-10 col-lg-5">
                    <form className="shadow-lg" onSubmit={submitHandler}>
                        <h1 className="mb-4">Direccion De Envio</h1>
                        <div className="form-group">
                            <label htmlFor="address_field">Direccion</label>
                            <input
                                type="text"
                                id="address_field"
                                className="form-control"
                                value={direccion ?? '' }
                                onChange={e => setDir(e.target.value)}
                                required
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="ciudad_field">Ciudad</label>
                            <input
                                type="text"
                                id="ciudad_field"
                                className="form-control"
                                value={ciudad ?? '' }
                                onChange={e => setCiudad(e.target.value)}
                                required
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="departamento_field">Departamento</label>
                            <input
                                type="text"
                                id="departamento_field"
                                className="form-control"
                                value={departamento ?? '' }
                                onChange={e => setDepartamento(e.target.value)}
                                required
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="codigoPostal_field">Codigo Postal</label>
                            <input
                                type="text"
                                id="codigoPostal_field"
                                className="form-control"
                                value={codigoPostal ?? '' }
                                onChange={e => setCodigoPostal(e.target.value)}
                                required
                            />
                        </div>


                        <div className="form-group">
                            <label htmlFor="country_field">Pais</label>
                            <select
                                id="country_field"
                                className="form-control"
                                value={pais ?? 'US' }
                                onChange={e => setPais(e.target.value)}
                                required
                                >
                                {
                                    countries.map(country => (
                                        <option key={country.id} value={country.name}>
                                            {country.name}
                                        </option>
                                    ))
                                }
                            </select>
                        </div>



                        <button
                            id="shipping_btn"
                            type="submit"
                            className="btn btn-block py-3"
                        >
                            CONTINUAR
                            </button>
                    </form>
                </div>
            </div>
    </Fragment>
  );
};

export default Shipping;
