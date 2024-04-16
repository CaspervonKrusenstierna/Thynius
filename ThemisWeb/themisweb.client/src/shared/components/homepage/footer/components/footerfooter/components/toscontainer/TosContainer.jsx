import React from 'react'
import "./TosContainer.css"
import TosContainerItem from './components/toscontaineritem/TosContainerItem'

const TosContainer = () => {
  return (
    <div className='TosContainer'>
        <TosContainerItem text="Personuppgiftspolicy" link="/privacy-policy"></TosContainerItem>
        <TosContainerItem text="Cookie Policy" link="/cookie-policy"></TosContainerItem>
    </div>
  )
}

export default TosContainer