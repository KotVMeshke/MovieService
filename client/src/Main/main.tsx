import React, { useState, useEffect } from 'react';
import moment from 'moment';

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
        <div>
            <button>Register</button>
            <button>Login</button>
            <input
                type="text"
                placeholder="Search..."
                value={searchTerm}
                onChange={(e) => { setSearchTerm(e.target.value) }}
            />
            <ul>
                {filteredObjects.map((object) => (
                    <li key={object.id}>
                        <h2>{object.name}</h2>
                        <p>{object.description}</p>
                        <p>{object.age}</p>
                        <p>{object.countryName}</p>
                        <p>{object.filmPath}</p>
                        <p>{object.posterPath}</p>
                        <p>{moment(object.releaseDate).format('YYYY')}</p>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default MyPage;