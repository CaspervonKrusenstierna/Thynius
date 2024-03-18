import React from 'react'
import "./InfoRowsContainer.css"
import InfoRow from './components/inforow/InfoRow'

const InfoRowsContainer = (props) => {
  return (
    <div className='InfoRowsContainer'>
        <p className='InfoRowsContainer-Title'>{props.title}</p>
        {props.items.map(item => {return <InfoRow text={item.ItemName} link={item.ItemLink}></InfoRow>})}
    </div>
  )
}

export default InfoRowsContainer