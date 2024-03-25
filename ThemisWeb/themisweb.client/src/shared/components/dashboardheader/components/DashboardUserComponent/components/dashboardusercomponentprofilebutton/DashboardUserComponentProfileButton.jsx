import React, {useContext, useEffect, useRef, useState} from 'react'
import { VectorDown, VectorUp } from '../../../../../../assets'
import { sessionInfoContext } from '../../../../../../../App';
import "./DashboardUserComponentProfileButton.css"

const DashboardUserComponentProfileButton = (props) => {
 const SessionInfo = useContext(sessionInfoContext);
 const [sizeState, setSizeState] = useState(true);
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

function onResize(){
  let width = window.innerWidth;


  if(width < 650){
    setSizeState(false);
  }
  else{
    setSizeState(true);
  }

}

useEffect(() => {
    onResize();
    window.addEventListener("resize", onResize);
}, []);


  return (
      <button className='DashboardUserComponentProfileButton' onClick={props.onClick}>
        {sizeState ?        
        <>
          <div className='DashboardUserComponentProfileButton-TextContainer'>
            <p className='DashboardUserComponentProfileButton-Text'>{SessionInfo?.Username}</p>
            <p className='DashboardUserComponentProfileButton-Text'>{UserRole.Current}</p>
          </div>
          <img src={props.clickState ? VectorUp : VectorDown} className='DashboardUserComponentProfileButton-ArrowIcon'></img>
        </>
         : <></>
          }

  </button>

  )
}

export default DashboardUserComponentProfileButton