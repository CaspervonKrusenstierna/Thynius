import React from 'react'
import { Footer, Header } from '../../shared/components/homepage'

const PrivacyPolicyPage = () => {
  return (
    <div className="flex-column">
        <Header></Header>  
        <div className='flex flex-col justify-self-center mt-[70px] ml-[20%] w-[60%] pb-[200px]'>
            <p className="font-bold text-[28px]">Personuppgiftpolicy</p>
            <p className='font-bold text-[24px] mt-8'>Introduktion</p>
            <p className='mt-4 text-[18px]'>
            Denna personuppgiftspolicy (”personuppgiftspolicyn”) gäller den personliga information som Thynius behandlar i samband med Thynius webbplatser, programvara och relaterade tjänster.
            </p>
            <p className='mt-4 text-[18px]'>
            Personuppgiftsansvarig: Thynius AB är personuppgiftsansvarig för den behandling som sker avseende dina personuppgifter om du bor i Europeiska ekonomiska samarbetsområdet (”EES”), Storbritannien (”UK”) eller Schweiz.
            </p>
            <p className='font-bold text-[24px] mt-8'>Informationen vi samlar in</p>
            <div className='flex flex-row pt-[11px]'>
                <p className='text-[25px]'> &bull; </p>
                <p className='ml-3 mt-[9px]'>
                    <span className='text-[18px] font-bold'>Profilinformation.</span>
                    <span className='text-[18px]'> Vi samlar in information som du lämnar när du skapar ett konto, Denna information innehåller ditt användarnamn, e-postadress och lösenord. Du kan sedan lägga till mer information i form av en profilbild.</span>
                </p>
            </div>
            <div className='flex flex-row pt-[11px]'>
                <p className='text-[25px]'> &bull; </p>
                <p className='ml-3 mt-[9px]'>
                    <span className='text-[18px] font-bold'>Användarinnehåll.</span>
                    <span className='text-[18px]'> Vi samlar in det innehåll som du skapar när du använder våra tjänster. Denna information innehåller dina texter som du har skrivit i Microsoft Word samt hur texterna är skrivna.</span>
                </p>
            </div>

            <p className='font-bold text-[24px] mt-8'>Hur vi använder din information</p>
            <p className='mt-4 text-[18px]'>Vi använder din information för att driva, tillhandahålla, utveckla och förbättra vår plattform, för följande ändamål:</p>
            <div className='flex flex-row pt-[11px]'>
                <p className='text-[25px]'> &bull; </p>
                <p className='ml-3 mt-[9px]'>
                    <span className='text-[18px]'> Tillhandahålla och administrera våran plattform. Så att elever kan dela sina texter med sina lärare, samt hur dessa texter är skrivna. Denna data lagras endast temporärt och blir raderad då läraren är färdig med sin analys av texten.</span>
                </p>
            </div>
            <div className='flex flex-row pt-[11px]'>
                <p className='text-[25px]'> &bull; </p>
                <p className='ml-3 mt-[9px]'>
                    <span className='text-[18px]'> Underhålla och förbättra plattformens säkerhet, trygghet och stabilitet genom att identifiera och åtgärda tekniska eller säkerhetsrelaterade frågor eller problem som t.ex. tekniska fel, skräppostkonton och upptäcka missbruk, bedrägeri och olaglig verksamhet.</span>
                </p>
            </div>
            <div className='flex flex-row pt-[11px]'>
                <p className='text-[25px]'> &bull; </p>
                <p className='ml-3 mt-[9px]'>
                    <span className='text-[18px]'> Uppfylla våra rättsliga förpliktelser, eller om det är nödvändigt för att utföra uppgifter i allmänhetens intresse.</span>
                </p>
            </div>

            <p className='font-bold text-[24px] mt-8'>Dina rättigheter och valmöjligheter</p>
            <p className='mt-4 text-[18px]'>Du har rättigheter och valmöjligheter då det kommer till din information, dessa är:</p>
            <div className='flex flex-row pt-[11px]'>
                <p className='text-[25px]'> &bull; </p>
                <p className='ml-3 mt-[9px]'>
                    <span className='text-[18px]'> Rättigheten att få tillgång till din information.</span>
                </p>
            </div>
            <div className='flex flex-row pt-[11px]'>
                <p className='text-[25px]'> &bull; </p>
                <p className='ml-3 mt-[9px]'>
                    <span className='text-[18px]'> Rättigheten att korrigera din information. Du kan be oss ändra eller korrigera din information om den inte är korrekt.</span>
                </p>
            </div>

            <div className='flex flex-row pt-[11px]'>
                <p className='text-[25px]'> &bull; </p>
                <p className='ml-3 mt-[9px]'>
                    <span className='text-[18px]'> Rättigheten att få de personuppgifter som du lämnat till oss överförda till en annan personuppgiftsansvarig (rätt till dataportabilitet).</span>
                </p>
            </div>

            <div className='flex flex-row pt-[11px]'>
                <p className='text-[25px]'> &bull; </p>
                <p className='ml-3 mt-[9px]'>
                    <span className='text-[18px]'> Om du är missnöjd med hur vi behandlar dina personuppgifter kan du göra en anmälan till Integritetsskyddsmyndigheten som är tillsynsmyndighet.</span>
                </p>
            </div>

            <div className='flex flex-row pt-[11px]'>
                <p className='text-[25px]'> &bull; </p>
                <p className='ml-3 mt-[9px]'>
                    <span className='text-[18px]'> Rättigheten att invända mot att vissa personuppgifter om dig behandlas samt begära att behandlingen av dina personuppgifter begränsas.</span>
                </p>
            </div>
            
            <p className='text-[18px] mt-6'>Om du vill utöva någon av ovanstående rättigheter så kan du kontakta oss kostnadsfritt via kontaktsformuläret som du finner längst ned på denna sida.</p>

            <p className='font-bold text-[24px] mt-8'>Hur vi använder din information</p>
            <p className='mt-[20px]'>
                    <span className='text-[18px]'>Det är viktigt för oss att din information är säker. Vi upprätthåller lämpliga tekniska, administrativa och fysiska säkerhetsåtgärder som är utformade för att skydda din information från obehörig åtkomst, stöld, röjande, ändring eller förlust. Vi ser även över våra säkerhetsåtgärder för att ta hänsyn till tillgänglig ny teknik och nya metoder.</span>
            </p>

            <p className='font-bold text-[24px] mt-8'>Uppdateringar av integritetspolicyn</p>
            <p className='mt-20px'>
            <span className='text-[18px]'>Thynius förbehåller sig rätten att revidera denna Personuppgiftspolicy från tid till annan. Vi kommer att meddela dig om eventuella väsentliga ändringar av denna policy genom ett meddelande som tillhandahålls via plattformen eller på annat sätt. Datumet ”Senast uppdaterad” längst upp i denna policy anger datumet då sådana policyändringar började gälla.</span>
            </p>
        </div>
        <Footer></Footer>
    </div>
  )
}

export default PrivacyPolicyPage