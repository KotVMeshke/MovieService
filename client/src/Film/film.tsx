import { useEffect, useState } from 'react';
import { useLocation, useParams } from 'react-router-dom';
import "./film.css";
import React from 'react';
import { useNavigate } from 'react-router-dom';

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
interface crewMember {
    id: number,
    role: string,
    name: string,
    surname: string
}

interface CommentData {
    userName: string,
    text: string,
    mark: number
}
const CommentCard = ({ userName, text, mark }: CommentData) => {
    return (
        <div className="comment-card">
            <div className="top-row">
                <h3 className="name">{userName}</h3>
                <span className="mark">{mark}</span>
            </div>
            <p className="comment">{text}</p>
        </div>
    );
};


const Film = () => {
    const [comments, setComments] = useState<CommentData[]>([]);
    const [userComment, setUserComment] = useState('');
    const [userMark, setUserMark] = useState('');
    const [crew, setCrew] = useState<crewMember[]>([]);

    const [film, setFilm] = useState<FilmData>();
    const [isTrailer, setIsTrailer] = useState(false);

    const togglePlayer = () => {
        setIsTrailer(!isTrailer);
    };
    const navigate = useNavigate();
    const { id } = useParams();

    const sendComment = async () => {
        console.log(localStorage.getItem("user_id"));
        console.log("adasdasdas");
        const response = await fetch(`http://localhost:5025/api/v1.0/feedback/add?userId=${localStorage.getItem("user_id")}&filmId=${id}&mark=${userMark}&text=${userComment}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },

        });

    }

    const handleCrewClick = (name: string) => {
        navigate('/')
    }
    useEffect(() => {
        const getComments = async () => {
            try {
                const response = await fetch(`http://localhost:5025/api/v1.0/feedback/${id}`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                });
                console.log(response.status);
                if (response.status == 302) {
                    const data = (await response.json()).data;
                    console.log('Comments getting successful:', data)
                    if (Array.isArray(data)) {
                        setComments(data);
                        console.log(comments);
                    } else {
                        console.error('Data is not an array:', data);
                    }
                } else {
                    console.error('Comments getting failed:', response.statusText);
                }
            } catch (error) {
                console.error('Error during getting Comments:', error);
            }
        }
        const getFilmData = async () => {
            try {
                const response = await fetch(`http://localhost:5025/api/v1.0/movie/${id}`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                });
                console.log(response.status);
                if (response.status == 302) {
                    const data = (await response.json()).data as FilmData;
                    console.log('Film getting successful:', data)
                    setFilm(data);
                    console.log(film);
                    console.log(film?.filmPath);

                } else {
                    console.error('Film getting failed:', response.statusText);
                }
            } catch (error) {
                console.error('Error during getting film:', error);
            }
        }

        const getCrewData = async () => {
            try {
                const response = await fetch(`http://localhost:5025/api/v1.0/movie/crew/${id}`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                });
                console.log(response.status);
                if (response.status == 302) {
                    const data = (await response.json()).data as crewMember[];
                    console.log('Crew getting successful:', data)
                    setCrew(data);
                    console.log('sda');
                    console.log(crew);

                } else {
                    console.error('Crew getting failed:', response.statusText);
                }
            } catch (error) {
                console.error('Error during getting crew:', error);
            }
        }
        getFilmData();
        getCrewData();
        getComments();
    }, []);
    return (
        <div className="film-container">
            <div className='just-div'>
                <div className="film-info-info">
                    <div className="film-info-upper">
                        <h2 className="film-info-name">Название: {film?.name} ({moment(film?.releaseDate).format('YYYY')})</h2>
                    </div>
                    <br />

                    <div className="film-info-poster-container">
                        <img className="film-info-poster" src={`http://localhost:5025/api/v1.0/images?path=${film?.posterPath}`} />
                        <div className='film-crew-info'>
                            
                        <span className='director'>
                                Режиссёр:
                                {crew
                                    .filter(member => member.role === 'Режиссер')
                                    .map((member, index) => (
                                        <React.Fragment key={`${member.id}`}>
                                            {index > 0 && ', '}
                                            <a href={`/crew/${member.id}`}>
                                                {`${member.name} ${member.surname}`}
                                            </a>
                                        </React.Fragment>
                                    ))
                                }
                                <br />
                                <br />

                            </span>
                            {/* <p className='director'>Режиссёр: <a href={`/crew/${crew}`}>{crew.find(member => member.role === 'Режиссер')?.name} {crew.find(member => member.role === 'Режиссер')?.surname}</a></p> */}
                            <span className='screenwriters'>
                                Сценаристы:
                                {crew
                                    .filter(member => member.role === 'Сценарист')
                                    .map((member, index) => (
                                        <React.Fragment key={`${member.id}`}>
                                            {index > 0 && ', '}
                                            <a href={`/crew/${member.id}`}>
                                                {`${member.name} ${member.surname}`}
                                            </a>
                                        </React.Fragment>
                                    ))
                                }
                         

                            </span>
                            <br />
                            <br />


                            <span className='actors'>
                                Актёры:
                                {crew
                                    .filter(member => member.role === 'Актёр')
                                    .map((member, index) => (
                                        <React.Fragment key={`${member.id}`}>
                                            {index > 0 && ', '}
                                            <a href={`/crew/${member.id}`}>
                                                {`${member.name} ${member.surname}`}
                                            </a>
                                        </React.Fragment>
                                    ))
                                }
                            </span>
                            
                           

                        </div>
                    </div>
                    <div className="film-info-lower">
                        <p className="film-info-description">Описание: {film?.description}</p>
                        <p className="film-info-restriction">Возрастное ограничение: {film?.age}</p>
                    </div>

                </div>
                <div>
                    {isTrailer && film ? (
                        <div>
                            <iframe className='video-player' src={film?.trailerPath}></iframe>
                            <button className='trailer-film-selector' onClick={togglePlayer}>Смотреть фильм</button>
                        </div>
                    ) : (
                        <div>
                            <video className="video-player" controls preload="metadata">
                                {film ? (
                                    <source src={`http://localhost:5025/api/v1.0/video?path=${film?.filmPath}`} type="video/mp4" />
                                ) : (
                                    ''
                                )}
                                Ваш браузер не поддерживает тег video.
                            </video>
                            <button className='trailer-film-selector' onClick={togglePlayer}>Смотреть трейлер</button>
                        </div>
                    )}
                </div>
                <div className="comments-container">
                    <h2 className='comment-h'>Comments</h2>
                    <div className="comment-input-container">
                        <input
                            type="text"
                            placeholder="Add a comment"
                            onChange={(e) => setUserComment(e.target.value)}
                            className="comment-input"
                        />
                        <input
                            type="number"
                            min="1"
                            max="10"
                            placeholder="Mark"
                            onChange={(e) => setUserMark(e.target.value)}
                            className="mark-input"
                        />
                        <button type="submit" onClick={sendComment} className="submit-button">
                            Submit
                        </button>

                    </div>
                    <ul className="comments-list">
                        {comments.map((comment) => (
                            <li key={comment.userName}>
                                <CommentCard {...comment} />
                            </li>
                        ))}
                    </ul>

                </div>
            </div>
        </div>
    );
}



export default Film;