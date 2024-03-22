import React, {useContext, useRef} from 'react'
import { VectorDown, VectorUp } from '../../../../../../assets'
import { sessionInfoContext } from '../../../../../../../App';
import "./DashboardUserComponentProfileButton.css"

const DashboardUserComponentProfileButton = (props) => {
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
    <button className='DashboardUserComponentProfileButton' onClick={props.onClick}>
    <img className='DashboardUserComponentProfileButton-Icon'></img>
    <div className='DashboardUserComponentProfileButton-TextContainer'>
      <p className='DashboardUserComponentProfileButton-Text'>{SessionInfo?.Username}</p>
      <p className='DashboardUserComponentProfileButton-Text'>{UserRole.Current}</p>
    </div>
    <img src={props.clickState ? VectorUp : VectorDown} className='DashboardUserComponentProfileButton-ArrowIcon'></img>
  </button>
  )
}

export default DashboardUserComponentProfileButton