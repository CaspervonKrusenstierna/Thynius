import React from 'react'
import "./BottomRightContainerButton.css"
import { Button } from "@/components/ui/button"
import { ArrowRightWhite } from '../../../assets'


const BottomRightContainerButton = (props) => {
  return (
    <Button className="BottomRightContainerButton" size="default" onClick={props.onClick}>
        <p className='text-lg'>Bekräfta</p>
    </Button>
  )
}
export default BottomRightContainerButton