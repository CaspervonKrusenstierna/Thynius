import React from 'react'
import { Footer, Header } from '../../shared/components/homepage'

const CookiesPage = () => {
  return (
    <>
        <Header></Header>  
        <div className='flex flex-col justify-self-center mt-[70px] ml-[20%] w-[60%] pb-[200px]'>
            <p className="font-bold text-[28px]">Cookie Policy</p>
                <p className='mt-4 text-[18px]'>
                För att maximera din upplevelse på hemsidan och i Thynius plattform, och ständigt jobba med förbättringar av våran tjänst, så lagrar vi olika typer av Cookies ("Kakor") i din webbläsare när du använder våra hemsida och Thynius plattform.
                </p>
                <p className='font-bold text-[24px] mt-8'>Nödvändiga</p>
                <p className='mt-4 text-[18px]'>
                    Dessa cookies behövs för att webbplatsen ska fungera tekniskt och funktionellt och måste därför accepteras för att kunna besöka sidan.
                </p>

                <p className='font-semibold text-[17px] mt-4'>AspNetCore.Identity.Application</p>
                <p className='text-[15px] mt-4 font-semibold'>Ändamål: Används för att logga in och autentisera dig då du använder dig av våran plattform och tjänst.</p>
        </div>
        <Footer></Footer>
    </>
  )
}

export default CookiesPage