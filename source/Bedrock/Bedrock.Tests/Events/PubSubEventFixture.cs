using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Bedrock.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bedrock.Tests.Events
{
    [TestClass]
    public class PubSubEventFixture
    {
        [TestMethod]
        public void CanSubscribeAndRaiseEvent()
        {
            TestablePubSubEvent<string> pubSubEvent = new TestablePubSubEvent<string>();
            bool published = false;
            pubSubEvent.Subscribe(delegate { published = true; }, ThreadOption.PublisherThread, true, delegate { return true; });
            pubSubEvent.Publish(null);

            Assert.IsTrue(published);
        }

        [TestMethod]
        public void CanSubscribeAndRaiseEventNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();
            bool published = false;
            pubSubEvent.Subscribe(delegate { published = true; }, ThreadOption.PublisherThread, true);
            pubSubEvent.Publish();

            Assert.IsTrue(published);
        }

        [TestMethod]
        public void CanSubscribeAndRaiseCustomEvent()
        {
            var customEvent = new TestablePubSubEvent<Payload>();
            Payload payload = new Payload();
            var action = new ActionHelper();
            customEvent.Subscribe(action.Action);

            customEvent.Publish(payload);

            Assert.AreSame(action.ActionArg<Payload>(), payload);
        }

        [TestMethod]
        public void CanHaveMultipleSubscribersAndRaiseCustomEvent()
        {
            var customEvent = new TestablePubSubEvent<Payload>();
            Payload payload = new Payload();
            var action1 = new ActionHelper();
            var action2 = new ActionHelper();
            customEvent.Subscribe(action1.Action);
            customEvent.Subscribe(action2.Action);

            customEvent.Publish(payload);

            Assert.AreSame(action1.ActionArg<Payload>(), payload);
            Assert.AreSame(action2.ActionArg<Payload>(), payload);
        }

        [TestMethod]
        public void CanHaveMultipleSubscribersAndRaiseEvent()
        {
            var customEvent = new TestablePubSubEvent();
            var action1 = new ActionHelper();
            var action2 = new ActionHelper();
            customEvent.Subscribe(action1.Action);
            customEvent.Subscribe(action2.Action);

            customEvent.Publish();

            Assert.IsTrue(action1.ActionCalled);
            Assert.IsTrue(action2.ActionCalled);
        }

        [TestMethod]
        public void SubscribeTakesExecuteDelegateThreadOptionAndFilter()
        {
            TestablePubSubEvent<string> pubSubEvent = new TestablePubSubEvent<string>();
            var action = new ActionHelper();
            pubSubEvent.Subscribe(action.Action);

            pubSubEvent.Publish("test");

            Assert.AreEqual("test", action.ActionArg<string>());

        }

        [TestMethod]
        public void FilterEnablesActionTarget()
        {
            TestablePubSubEvent<string> pubSubEvent = new TestablePubSubEvent<string>();
            var goodFilter = new MockFilter { FilterReturnValue = true };
            var actionGoodFilter = new ActionHelper();
            var badFilter = new MockFilter { FilterReturnValue = false };
            var actionBadFilter = new ActionHelper();
            pubSubEvent.Subscribe(actionGoodFilter.Action, ThreadOption.PublisherThread, true, goodFilter.FilterString);
            pubSubEvent.Subscribe(actionBadFilter.Action, ThreadOption.PublisherThread, true, badFilter.FilterString);

            pubSubEvent.Publish("test");

            Assert.IsTrue(actionGoodFilter.ActionCalled);
            Assert.IsFalse(actionBadFilter.ActionCalled);

        }

        [TestMethod]
        public void SubscribeDefaultsThreadOptionAndNoFilter()
        {
            TestablePubSubEvent<string> pubSubEvent = new TestablePubSubEvent<string>();
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            SynchronizationContext calledSyncContext = null;
            var myAction = new ActionHelper()
            {
                ActionToExecute =
                    () => calledSyncContext = SynchronizationContext.Current
            };
            pubSubEvent.Subscribe(myAction.Action);

            pubSubEvent.Publish("test");

            Assert.AreEqual(SynchronizationContext.Current, calledSyncContext);
        }

        [TestMethod]
        public void SubscribeDefaultsThreadOptionAndNoFilterNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            SynchronizationContext calledSyncContext = null;
            var myAction = new ActionHelper()
            {
                ActionToExecute =
                    () => calledSyncContext = SynchronizationContext.Current
            };
            pubSubEvent.Subscribe(myAction.Action);

            pubSubEvent.Publish();

            Assert.AreEqual(SynchronizationContext.Current, calledSyncContext);
        }

        [TestMethod]
        public void ShouldUnsubscribeFromPublisherThread()
        {
            var PubSubEvent = new TestablePubSubEvent<string>();

            var actionEvent = new ActionHelper();
            PubSubEvent.Subscribe(
                actionEvent.Action,
                ThreadOption.PublisherThread);

            Assert.IsTrue(PubSubEvent.Contains(actionEvent.Action));
            PubSubEvent.Unsubscribe(actionEvent.Action);
            Assert.IsFalse(PubSubEvent.Contains(actionEvent.Action));
        }

        [TestMethod]
        public void ShouldUnsubscribeFromPublisherThreadNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();

            var actionEvent = new ActionHelper();
            pubSubEvent.Subscribe(
                actionEvent.Action,
                ThreadOption.PublisherThread);

            Assert.IsTrue(pubSubEvent.Contains(actionEvent.Action));
            pubSubEvent.Unsubscribe(actionEvent.Action);
            Assert.IsFalse(pubSubEvent.Contains(actionEvent.Action));
        }

        [TestMethod]
        public void UnsubscribeShouldNotFailWithNonSubscriber()
        {
            TestablePubSubEvent<string> pubSubEvent = new TestablePubSubEvent<string>();

            Action<string> subscriber = delegate { };
            pubSubEvent.Unsubscribe(subscriber);
        }

        [TestMethod]
        public void UnsubscribeShouldNotFailWithNonSubscriberNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();

            Action subscriber = delegate { };
            pubSubEvent.Unsubscribe(subscriber);
        }

        [TestMethod]
        public void ShouldUnsubscribeFromBackgroundThread()
        {
            var PubSubEvent = new TestablePubSubEvent<string>();

            var actionEvent = new ActionHelper();
            PubSubEvent.Subscribe(
                actionEvent.Action,
                ThreadOption.BackgroundThread);

            Assert.IsTrue(PubSubEvent.Contains(actionEvent.Action));
            PubSubEvent.Unsubscribe(actionEvent.Action);
            Assert.IsFalse(PubSubEvent.Contains(actionEvent.Action));
        }

        [TestMethod]
        public void ShouldUnsubscribeFromBackgroundThreadNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();

            var actionEvent = new ActionHelper();
            pubSubEvent.Subscribe(
                actionEvent.Action,
                ThreadOption.BackgroundThread);

            Assert.IsTrue(pubSubEvent.Contains(actionEvent.Action));
            pubSubEvent.Unsubscribe(actionEvent.Action);
            Assert.IsFalse(pubSubEvent.Contains(actionEvent.Action));
        }

        [TestMethod]
        public void ShouldUnsubscribeFromUIThread()
        {
            var PubSubEvent = new TestablePubSubEvent<string>();
            PubSubEvent.SynchronizationContext = new SynchronizationContext();

            var actionEvent = new ActionHelper();
            PubSubEvent.Subscribe(
                actionEvent.Action,
                ThreadOption.UIThread);

            Assert.IsTrue(PubSubEvent.Contains(actionEvent.Action));
            PubSubEvent.Unsubscribe(actionEvent.Action);
            Assert.IsFalse(PubSubEvent.Contains(actionEvent.Action));
        }

        [TestMethod]
        public void ShouldUnsubscribeFromUIThreadNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();
            pubSubEvent.SynchronizationContext = new SynchronizationContext();

            var actionEvent = new ActionHelper();
            pubSubEvent.Subscribe(
                actionEvent.Action,
                ThreadOption.UIThread);

            Assert.IsTrue(pubSubEvent.Contains(actionEvent.Action));
            pubSubEvent.Unsubscribe(actionEvent.Action);
            Assert.IsFalse(pubSubEvent.Contains(actionEvent.Action));
        }

        [TestMethod]
        public void ShouldUnsubscribeASingleDelegate()
        {
            var PubSubEvent = new TestablePubSubEvent<string>();

            int callCount = 0;

            var actionEvent = new ActionHelper() { ActionToExecute = () => callCount++ };
            PubSubEvent.Subscribe(actionEvent.Action);
            PubSubEvent.Subscribe(actionEvent.Action);

            PubSubEvent.Publish(null);
            Assert.AreEqual<int>(2, callCount);

            callCount = 0;
            PubSubEvent.Unsubscribe(actionEvent.Action);
            PubSubEvent.Publish(null);
            Assert.AreEqual<int>(1, callCount);
        }

        [TestMethod]
        public void ShouldUnsubscribeASingleDelegateNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();

            int callCount = 0;

            var actionEvent = new ActionHelper() { ActionToExecute = () => callCount++ };
            pubSubEvent.Subscribe(actionEvent.Action);
            pubSubEvent.Subscribe(actionEvent.Action);

            pubSubEvent.Publish();
            Assert.AreEqual<int>(2, callCount);

            callCount = 0;
            pubSubEvent.Unsubscribe(actionEvent.Action);
            pubSubEvent.Publish();
            Assert.AreEqual<int>(1, callCount);
        }

        [TestMethod]
        public void ShouldNotExecuteOnGarbageCollectedDelegateReferenceWhenNotKeepAlive()
        {
            var PubSubEvent = new TestablePubSubEvent<string>();

            ExternalAction externalAction = new ExternalAction();
            PubSubEvent.Subscribe(externalAction.ExecuteAction);

            PubSubEvent.Publish("testPayload");
            Assert.AreEqual("testPayload", externalAction.PassedValue);

            WeakReference actionEventReference = new WeakReference(externalAction);
            externalAction = null;
            GC.Collect();
            Assert.IsFalse(actionEventReference.IsAlive);

            PubSubEvent.Publish("testPayload");
        }

        [TestMethod]
        public void ShouldNotExecuteOnGarbageCollectedDelegateReferenceWhenNotKeepAliveNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();

            var externalAction = new ExternalAction();
            pubSubEvent.Subscribe(externalAction.ExecuteAction);

            pubSubEvent.Publish();
            Assert.IsTrue(externalAction.Executed);

            var actionEventReference = new WeakReference(externalAction);
            externalAction = null;
            GC.Collect();
            Assert.IsFalse(actionEventReference.IsAlive);

            pubSubEvent.Publish();
        }

        [TestMethod]
        public void ShouldNotExecuteOnGarbageCollectedFilterReferenceWhenNotKeepAlive()
        {
            var PubSubEvent = new TestablePubSubEvent<string>();

            bool wasCalled = false;
            var actionEvent = new ActionHelper() { ActionToExecute = () => wasCalled = true };

            ExternalFilter filter = new ExternalFilter();
            PubSubEvent.Subscribe(actionEvent.Action, ThreadOption.PublisherThread, false, filter.AlwaysTrueFilter);

            PubSubEvent.Publish("testPayload");
            Assert.IsTrue(wasCalled);

            wasCalled = false;
            WeakReference filterReference = new WeakReference(filter);
            filter = null;
            GC.Collect();
            Assert.IsFalse(filterReference.IsAlive);

            PubSubEvent.Publish("testPayload");
            Assert.IsFalse(wasCalled);
        }

        [TestMethod]
        public void CanAddSubscriptionWhileEventIsFiring()
        {
            var PubSubEvent = new TestablePubSubEvent<string>();

            var emptyAction = new ActionHelper();
            var subscriptionAction = new ActionHelper
            {
                ActionToExecute = (() =>
                                                  PubSubEvent.Subscribe(
                                                      emptyAction.Action))
            };

            PubSubEvent.Subscribe(subscriptionAction.Action);

            Assert.IsFalse(PubSubEvent.Contains(emptyAction.Action));

            PubSubEvent.Publish(null);

            Assert.IsTrue((PubSubEvent.Contains(emptyAction.Action)));
        }

        [TestMethod]
        public void CanAddSubscriptionWhileEventIsFiringNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();

            var emptyAction = new ActionHelper();
            var subscriptionAction = new ActionHelper
            {
                ActionToExecute = (() =>
                                          pubSubEvent.Subscribe(
                                          emptyAction.Action))
            };

            pubSubEvent.Subscribe(subscriptionAction.Action);

            Assert.IsFalse(pubSubEvent.Contains(emptyAction.Action));

            pubSubEvent.Publish();

            Assert.IsTrue((pubSubEvent.Contains(emptyAction.Action)));
        }

        [TestMethod]
        public void InlineDelegateDeclarationsDoesNotGetCollectedIncorrectlyWithWeakReferences()
        {
            var PubSubEvent = new TestablePubSubEvent<string>();
            bool published = false;
            PubSubEvent.Subscribe(delegate { published = true; }, ThreadOption.PublisherThread, false, delegate { return true; });
            GC.Collect();
            PubSubEvent.Publish(null);

            Assert.IsTrue(published);
        }

        [TestMethod]
        public void InlineDelegateDeclarationsDoesNotGetCollectedIncorrectlyWithWeakReferencesNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();
            bool published = false;
            pubSubEvent.Subscribe(delegate { published = true; }, ThreadOption.PublisherThread, false);
            GC.Collect();
            pubSubEvent.Publish();

            Assert.IsTrue(published);
        }

        [TestMethod]
        public void ShouldNotGarbageCollectDelegateReferenceWhenUsingKeepAlive()
        {
            var PubSubEvent = new TestablePubSubEvent<string>();

            var externalAction = new ExternalAction();
            PubSubEvent.Subscribe(externalAction.ExecuteAction, ThreadOption.PublisherThread, true);

            WeakReference actionEventReference = new WeakReference(externalAction);
            externalAction = null;
            GC.Collect();
            GC.Collect();
            Assert.IsTrue(actionEventReference.IsAlive);

            PubSubEvent.Publish("testPayload");

            Assert.AreEqual("testPayload", ((ExternalAction)actionEventReference.Target).PassedValue);
        }

        [TestMethod]
        public void ShouldNotGarbageCollectDelegateReferenceWhenUsingKeepAliveNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();

            var externalAction = new ExternalAction();
            pubSubEvent.Subscribe(externalAction.ExecuteAction, ThreadOption.PublisherThread, true);

            WeakReference actionEventReference = new WeakReference(externalAction);
            externalAction = null;
            GC.Collect();
            GC.Collect();
            Assert.IsTrue(actionEventReference.IsAlive);

            pubSubEvent.Publish();

            Assert.IsTrue(((ExternalAction)actionEventReference.Target).Executed);
        }

        [TestMethod]
        public void RegisterReturnsTokenThatCanBeUsedToUnsubscribe()
        {
            var PubSubEvent = new TestablePubSubEvent<string>();
            var emptyAction = new ActionHelper();

            var token = PubSubEvent.Subscribe(emptyAction.Action);
            PubSubEvent.Unsubscribe(token);

            Assert.IsFalse(PubSubEvent.Contains(emptyAction.Action));
        }

        [TestMethod]
        public void RegisterReturnsTokenThatCanBeUsedToUnsubscribeNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();
            var emptyAction = new ActionHelper();

            var token = pubSubEvent.Subscribe(emptyAction.Action);
            pubSubEvent.Unsubscribe(token);

            Assert.IsFalse(pubSubEvent.Contains(emptyAction.Action));
        }

        [TestMethod]
        public void ContainsShouldSearchByToken()
        {
            var PubSubEvent = new TestablePubSubEvent<string>();
            var emptyAction = new ActionHelper();
            var token = PubSubEvent.Subscribe(emptyAction.Action);

            Assert.IsTrue(PubSubEvent.Contains(token));

            PubSubEvent.Unsubscribe(emptyAction.Action);
            Assert.IsFalse(PubSubEvent.Contains(token));
        }

        [TestMethod]
        public void ContainsShouldSearchByTokenNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();
            var emptyAction = new ActionHelper();
            var token = pubSubEvent.Subscribe(emptyAction.Action);

            Assert.IsTrue(pubSubEvent.Contains(token));

            pubSubEvent.Unsubscribe(emptyAction.Action);
            Assert.IsFalse(pubSubEvent.Contains(token));
        }

        [TestMethod]
        public void SubscribeDefaultsToPublisherThread()
        {
            var PubSubEvent = new TestablePubSubEvent<string>();
            Action<string> action = delegate { };
            var token = PubSubEvent.Subscribe(action, true);

            Assert.AreEqual(1, PubSubEvent.BaseSubscriptions.Count);
            Assert.AreEqual(typeof(EventSubscription<string>), PubSubEvent.BaseSubscriptions.ElementAt(0).GetType());
        }

        [TestMethod]
        public void SubscribeDefaultsToPublisherThreadNonGeneric()
        {
            var pubSubEvent = new TestablePubSubEvent();
            Action action = delegate { };
            var token = pubSubEvent.Subscribe(action, true);

            Assert.AreEqual(1, pubSubEvent.BaseSubscriptions.Count);
            Assert.AreEqual(typeof(EventSubscription), pubSubEvent.BaseSubscriptions.ElementAt(0).GetType());
        }

        public class ExternalFilter
        {
            public bool AlwaysTrueFilter(string value)
            {
                return true;
            }
        }

        public class ExternalAction
        {
            public string PassedValue;
            public bool Executed = false;

            public void ExecuteAction(string value)
            {
                PassedValue = value;
                Executed = true;
            }

            public void ExecuteAction()
            {
                Executed = true;
            }
        }

        class TestablePubSubEvent<TPayload> : PubSubEvent<TPayload>
        {
            public ICollection<IEventSubscription> BaseSubscriptions
            {
                get { return base.Subscriptions; }
            }
        }

        class TestablePubSubEvent : PubSubEvent
        {
            public ICollection<IEventSubscription> BaseSubscriptions
            {
                get { return base.Subscriptions; }
            }
        }

        public class Payload { }
    }

    public class ActionHelper
    {
        public bool ActionCalled;
        public Action ActionToExecute = null;
        private object actionArg;

        public T ActionArg<T>()
        {
            return (T)actionArg;
        }

        public void Action(PubSubEventFixture.Payload arg)
        {
            Action((object)arg);
        }

        public void Action(string arg)
        {
            Action((object)arg);
        }

        public void Action(object arg)
        {
            actionArg = arg;
            ActionCalled = true;
            if (ActionToExecute != null)
            {
                ActionToExecute.Invoke();
            }
        }

        public void Action()
        {
            ActionCalled = true;
            if (ActionToExecute != null)
            {
                ActionToExecute.Invoke();
            }
        }
    }

    public class MockFilter
    {
        public bool FilterReturnValue;

        public bool FilterString(string arg)
        {
            return FilterReturnValue;
        }
    }
}