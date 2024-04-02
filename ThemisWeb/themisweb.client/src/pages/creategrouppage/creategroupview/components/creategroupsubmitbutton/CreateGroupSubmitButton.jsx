import React from 'react'
import "./CreateGroupSubmitButton.css"
import { Button } from "@/components/ui/button"
import { ArrowRight } from '../../../../../shared/assets'


const CreateGroupSubmitButton = () => {
  return (
    <Button className="CreateGroupSubmitButton" size="default">
        <p className='text-lg'>Skapa</p>
        <img className="h-6"src={ArrowRight}></img>
    </Button>
  )
}
export default CreateGroupSubmitButton