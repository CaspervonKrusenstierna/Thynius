import React from 'react'
import "./TosContainer.css"
import TosContainerItem from './components/toscontaineritem/TosContainerItem'

const TosContainer = () => {
  return (
    <div className='TosContainer'>
        <TosContainerItem text="Terms of Service" link="/pages/terms-of-service"></TosContainerItem>
        <TosContainerItem text="Privacy Policy" link="/pages/privacy-policy"></TosContainerItem>
        <TosContainerItem text="Do Not Sell My Information" link="/pages/ccpa"></TosContainerItem>
    </div>
  )
}

export default TosContainer