import React from 'react'
import "./MainContentContainer.css"
const MainContentContainer = () => {
  return (
    <div className='MainContentContainer'>
      <div className='flex flex-row justify-start w-full h-[600px]'>
        <p className='mt-[150px] text-[48px] leading-[56px] font-bold'>Skapa en rättvist skola <br/> <span className='text-[var(--themeMainColor)]'>För alla.</span></p>
      </div>

      <div className='flex flex-row justify-start w-[93%] h-[600px]'>
        <div className='flex flex-col mt-[120px] gap-5 '>
          <p className='text-[40px] font-bold leading-[44px] '>En ny väg för att <br/> motverka fuskandet.</p>
          <p className='text-[24px] leading-[30px]'>Thynius tänker utanför texten.<br/> Istället för att analysera texten när den redan är färdigskriven <br/> analyserar Thynius hur texten är skriven. <br/> Detta skapar möjligheter som ej funnits tidigare. </p>
        </div>
        
      </div>
    </div>
  )
}

export default MainContentContainer