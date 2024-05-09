export default function useSubmittmentRawText(rawTextURL){
    const [rawText, setRawText] = useState();
    useEffect(() => {
        async function getText(){
          await fetch(rawTextURL).then(s => s.text()).then(s => {console.log(s); setRawText(s)});
        }
        getText();
      }, [submittmentInfo.links])
      return rawText();
}