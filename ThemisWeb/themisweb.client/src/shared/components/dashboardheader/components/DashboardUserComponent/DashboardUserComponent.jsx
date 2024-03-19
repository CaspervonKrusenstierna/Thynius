import React, { useEffect, useRef } from 'react'
import { useContext } from 'react'
import { sessionInfoContext } from '../../../../../App'
import "./DashboardUserComponent.css"
import { VectorDown } from '../../../../assets'

const DashboardUserComponent = () => {
  const SessionInfo = useContext(sessionInfoContext);
  const UserRole = useRef("");

  switch(SessionInfo?.RoleLevel){
    case 3:
      UserRole.Current = "Administratör"; break;
    case 2:
      UserRole.Current = "Administratör"; break;
    case 1:
      UserRole.Current = "Lärare"; break;
    default:
      UserRole.Current = "Elev"
  }

  return (
    <button className='DashboardUserComponent'>
      <img className='DashboardUserComponent-Icon'></img>
      <div className='DashboardUserComponent-TextContainer'>
        <p className='DashboardUserComponent-Text'>{SessionInfo?.Username}</p>
        <p className='DashboardUserComponent-Text'>{UserRole.Current}</p>
      </div>
      <img src={VectorDown} className='DashboardUserComponent-ArrowIcon'></img>
    </button>
  )
}

export default DashboardUserComponent