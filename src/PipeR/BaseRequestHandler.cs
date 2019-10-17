﻿namespace PipeR
{
    public abstract class BaseRequestHandler<TContext, TRequest> : IRequestHandler<TContext, TRequest>
        where TContext : BaseContext
    {
        public IRequestHandler<TContext, TRequest> NextRequestHandler { get; set; }
        public TContext Context { get; private set; }

        protected BaseRequestHandler(TContext context)
        {
            this.Context = context;
        }

        protected RequestHandlerResult Next(TRequest request)
        {
            if (NextRequestHandler != null)
                this.Context.Response = NextRequestHandler.HandleRequest(request);

            return this.Context.Response;
        }

        protected RequestHandlerResult Abort(string errorMessage)
            => this.Context.Response = new RequestHandlerResult(errorMessage);


        protected RequestHandlerResult Finish(object result)
            => this.Context.Response = new RequestHandlerResult(result);


        public abstract RequestHandlerResult HandleRequest(TRequest request);
    }

    public interface IRequestHandler<TContext, TRequest> where TContext : BaseContext
    {
        RequestHandlerResult HandleRequest(TRequest request);
        IRequestHandler<TContext, TRequest> NextRequestHandler { get; set; }
        TContext Context { get; }
    }
}