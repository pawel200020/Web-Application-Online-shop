import Link from "next/link";

export default function Register(){
    return(
        <section id='register' className='main-page-container'>
            <div className="flex justify-center items-center " data-theme="light dark">
                <div className="card w-96 bg-base-200 text-base-content shadow-xl p-6">
                    <h2 className="text-2xl font-bold text-center mb-4">Register</h2>

                    <div className="form-control w-full">
                        <label className="label">
                            <span className="label-text">First Name</span>
                        </label>
                        <input type="text" placeholder="Enter your first name" className="input input-bordered w-full"/>
                    </div>

                    <div className="form-control w-full mt-3">
                        <label className="label">
                            <span className="label-text">Last Name</span>
                        </label>
                        <input type="text" placeholder="Enter your last name" className="input input-bordered w-full"/>
                    </div>

                    <div className="form-control w-full mt-3">
                        <label className="label">
                            <span className="label-text">Email</span>
                        </label>
                        <input type="email" placeholder="Enter your email" className="input input-bordered w-full"/>
                    </div>

                    <div className="form-control w-full mt-3">
                        <label className="label">
                            <span className="label-text">Birth Date</span>
                        </label>
                        <input type="date" className="input input-bordered w-full"/>
                    </div>

                    <div className="form-control w-full mt-3">
                        <label className="label">
                            <span className="label-text">Phone Number</span>
                        </label>
                        <input type="tel" placeholder="Enter your phone number"
                               className="input input-bordered w-full"/>
                    </div>

                    <div className="form-control w-full mt-3">
                        <label className="label">
                            <span className="label-text">Password</span>
                        </label>
                        <input type="password" placeholder="Enter your password"
                               className="input input-bordered w-full"/>
                    </div>

                    <div className="form-control w-full mt-3">
                        <label className="label">
                            <span className="label-text">Confirm Password</span>
                        </label>
                        <input type="password" placeholder="Confirm your password"
                               className="input input-bordered w-full"/>
                    </div>

                    <button className="btn btn-primary w-full mt-4">Register</button>
                    <p className="text-center text-sm mt-2">
                        Already have an account? <Link href="/login/" className="text-primary hover:underline">Login</Link>
                    </p>
                </div>
            </div>
        </section>
    )
}