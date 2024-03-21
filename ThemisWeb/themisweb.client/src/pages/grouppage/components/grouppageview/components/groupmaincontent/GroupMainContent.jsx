import React, { useEffect, useState } from 'react'
import "./GroupMainContent.css"

const GroupMainContent = (props) => {
  const [pageContent, setPageContent] = useState(<></>);
  useEffect(() => {
    switch(props.Page){
      case "Home":
        setPageContent(<div>yaba</div>); break;
        
      case "Assignments":
        setPageContent(<div>daba</div>); break;
    }
  }, [props.Page]);
  return (
    <div className='GroupMainContent'>{pageContent}</div>
  )
}

export default GroupMainContent