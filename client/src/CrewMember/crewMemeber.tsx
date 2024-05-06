import moment from 'moment';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useLocation, useParams } from 'react-router-dom';
import "./crewMember.css";

interface FullCrewMember {
    name: string,
    surname: string,
    patronymic: string,
    photoPath: string,
    age: number,
    date: Date
}

const CrewMember = () => {
    const { crewId } = useParams();
    console.log(crewId);
    const [crewMemeber, setCrewMember] = useState<FullCrewMember>();

    useEffect(() => {


        const getCrewData = async () => {
            try {
                const response = await fetch(`http://localhost:5025/api/v1.0/crew/${crewId}`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                });
                console.log(response.status);
                if (response.status == 302) {
                    const data = (await response.json()).data as FullCrewMember;
                    console.log('Crew getting successful:', data)
                    setCrewMember(data);
                    console.log('sda');
                    console.log(crewMemeber);

                } else {
                    console.error('Crew getting failed:', response.statusText);
                }
            } catch (error) {
                console.error('Error during getting crew:', error);
            }
        }
        getCrewData();
    }, []);

    return (
        <div className="crew-member">
            <div className='just-div-v2'>
                <h2 className='crew-member-full-name'>{crewMemeber?.name} {crewMemeber?.surname} {crewMemeber?.patronymic}</h2>
                <div className="crew-member-details">
                    <div className="crew-member-photo">
                        <img src={`http://localhost:5025/api/v1.0/images?path=${crewMemeber?.photoPath}`} alt="Фото" />
                    </div>
                    <div className="crew-member-info">
                        <p className='crew-member-age'>Возраст: {crewMemeber?.age}</p>
                        <p className='crew-member-date'>Дата рождения: {moment(crewMemeber?.date).format('DD-MM-YYYY')}</p>
                    </div>
                </div>
            </div>

        </div>
    );
};

export default CrewMember;