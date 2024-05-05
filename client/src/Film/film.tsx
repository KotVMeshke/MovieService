import { useEffect, useState } from 'react';
import { useLocation, useParams } from 'react-router-dom';
import "./film.css";
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
    const [film, setFilm] = useState<FilmData>();

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
        getFilmData();
        getComments();
    }, []);
    return (
        <div className="container">
            <div className='film-info'>
                <img className="film-poster" src={`http://localhost:5025/api/v1.0/images?path=${film?.posterPath}`} />
                <div className="film-details">
                    <div className="film-upper">
                        <h2 className="film-name">Название: {film?.name} ({moment(film?.releaseDate).format('YYYY')})</h2>
                    </div>
                    <div className="film-left-side">
                        <p className="film-description">Описание: {film?.description}</p>
                        <p className="film-restriction">Возрастное ограничение: {film?.age}</p>
                       
                    </div>
                </div>
            </div>
            <div>
                <video className="video-player" controls preload="metadata">
                    {film ? (
                        <source src={`http://localhost:5025/api/v1.0/video?path=${film?.filmPath}`} type="video/mp4 " />
                    ) : (
                        ''
                    )}
                    Your browser does not support the video tag.
                </video>
            </div>
            <div className="comments-container">
                <h2>Comments</h2>
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
    );
}



export default Film;