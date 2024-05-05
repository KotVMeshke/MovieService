import React, { useState, useEffect } from 'react';
import moment from 'moment';
import './main.css'
import { useNavigate } from 'react-router-dom';

interface FilmData {
    id: number,
    name: string,
    description: string,
    releaseDate: Date,
    age: string,
    trailerPath: string,
    filmPath: string,
    countryName: string,
    posterPath: string
}

const MyPage = () => {
    const [searchTerm, setSearchTerm] = useState('');
    const [objects, setObjects] = useState<FilmData[]>([]);
    const navigate = useNavigate();
    const handelLoginClick = () => {
        navigate("/login");
    }

    const handelLibraryClick = () => {
        navigate("/library");
    }

    const handelLogOutClick = () => {
        localStorage.removeItem("authorized");
        navigate("/");
    }

    const addToLibrary = async (id: number) => {
        console.log(localStorage.getItem("user_id"));
        const response = await fetch(`http://localhost:5025/api/v1.0/library/add?userId=${localStorage.getItem("user_id")}&filmId=${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
        });
    }

    const handelRegistrClick = () => {
        navigate("/registration");
    }
    useEffect(() => {
        const getFilms = async () => {
            try {
                const response = await fetch(`http://localhost:5025/api/v1.0/movie?offset=0&limit=20`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                });
                console.log(response.status);
                if (response.status == 302) {
                    const data = (await response.json()).data;
                    console.log('Films getting successful:', data)
                    if (Array.isArray(data)) {
                        setObjects(data);
                        console.log(objects);
                    } else {
                        console.error('Data is not an array:', data);
                    }
                } else {
                    console.error('Films getting failed:', response.statusText);
                }
            } catch (error) {
                console.error('Error during getting films:', error);
            }
        }

        getFilms();
    }, []);

    const filteredObjects = objects.filter((object) =>
        object.name.toLowerCase().includes(searchTerm.toLowerCase())
    );

    return (
        <div className="container">
            {localStorage.getItem('authorized') ? (
                <div className="button-group">
                    <button className="library-button" onClick={handelLibraryClick}>Library</button>
                    <button className="logout-button" onClick={handelLogOutClick} >Logout</button>
                </div>
            ) : (
                <div className="button-group">
                    <button className="registr-button" onClick={handelRegistrClick}>Register</button>
                    <button className="login-button" onClick={handelLoginClick}>Login</button>
                </div>
            )}
            <div className="search-panel">
                <input
                    className="search-input"
                    type="text"
                    placeholder="Search..."
                    value={searchTerm}
                    onChange={(e) => {
                        setSearchTerm(e.target.value);
                    }}
                />
            </div>
            <ul className="films-list">
                {filteredObjects.map((object) => (
                    <li className="film-card" key={object.id}>
                        <img className="film-poster" src={`http://localhost:5025/api/v1.0/images?path=${object.posterPath}`} />
                        <div className="film-details">
                            <div className="film-upper">
                                <h2 className="film-name">Название: {object.name} ({moment(object.releaseDate).format('YYYY')})</h2>
                            </div>
                            <div className="film-left-side">
                                <p className="film-description">Описание: {object.description}</p>
                                <p className="film-restriction">Возрастное ограничение: {object.age}</p>
                                {localStorage.getItem('authorized') ? (
                                    <button className="add-to-favorites" onClick={() => addToLibrary(object.id)}
                                    > <span className="heart-icon">&#9825;</span>
                                    </button>) : (
                                    null
                                )}
                            </div>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default MyPage;