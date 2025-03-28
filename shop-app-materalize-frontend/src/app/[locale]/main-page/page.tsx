import {getTranslations} from 'next-intl/server';

export default async function MainPage() {
    const t = await getTranslations('HomePage');
    console.log(t('title'))
    return (<section id='MainPage' className='main-page-container'>
        Hello<br/>
        {t('title')}
        fsfasfd<br/>

        asdfasdf<br/>

        asfasdf<br/>

        afa<br/>
        Hello<br/>

        fsfasfd<br/>

        asdfasdf<br/>

        asfasdf<br/>

        afa<br/>
        Hello<br/>

        fsfasfd<br/>

        asdfasdf<br/>

        asfasdf<br/>

        afa<br/>
        Hello<br/>

        fsfasfd<br/>

        asdfasdf<br/>

        asfasdf<br/>

        afa<br/>
        Hello<br/>

        fsfasfd<br/>

        asdfasdf<br/>

        asfasdf<br/>

        afa<br/>
        Hello<br/>

        fsfasfd<br/>

        asdfasdf<br/>

        asfasdf<br/>

        afa<br/>
        Hello<br/>

        fsfasfd<br/>

        asdfasdf<br/>

        asfasdf<br/>

        afa<br/>
        Hello<br/>

        fsfasfd<br/>

        asdfasdf<br/>

        asfasdf<br/>

        afa<br/>
        afa<br/>
    </section>)
}