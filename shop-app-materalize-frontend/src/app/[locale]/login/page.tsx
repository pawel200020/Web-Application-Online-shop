import {Link} from '@/i18n/navigation';

export default function Login () {
    return(<section id='login' className='main-page-container'>
        <div className="flex justify-center items-center min-h-[500px] " data-theme="light dark">
            <div className="card w-96 bg-base-200 text-base-content shadow-xl p-6">
                <h2 className="text-2xl font-bold text-center mb-4">Login</h2>
                <div className="form-control w-full">
                    <label className="label">
                        <span className="label-text">Email</span>
                    </label>
                    <input type="email" placeholder="Enter your email" className="input input-bordered w-full"/>
                </div>
                <div className="form-control w-full mt-3">
                    <label className="label">
                        <span className="label-text">Password</span>
                    </label>
                    <input type="password" placeholder="Enter your password" className="input input-bordered w-full"/>
                </div>
                <div className="flex justify-between mt-2 text-sm">
                    <a href="#" className="text-primary hover:underline">Forgot Password?</a>
                </div>
                <button className="btn btn-primary w-full mt-4">Login</button>
                <p className="text-center text-sm mt-2">
                    Dont have an account? <Link href="/register" className="text-primary hover:underline">Sign
                    up</Link>
                </p>
            </div>
        </div>
    </section>)
}